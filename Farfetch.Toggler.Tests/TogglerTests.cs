using System;
using System.Collections.Generic;
using System.Linq;
using Farfetch.Common;
using Farfetch.DB;
using Farfetch.DB.MongoDB;
using Farfetch.Models;
using Farfetch.Repositories;
using Farfetch.Repositories.MongoDB;
using Farfetch.ServiceFactory;
using Farfetch.Tests;
using Farfetch.Toggler.Service;
using MongoDB.Driver;
using NUnit.Framework;

namespace Farfetch.Toggler.Tests
{
    [TestFixture]
    public class TogglerTests: TestBase
    {
        [Test, Order(1)]
        public void ShouldInsertService()
        {
            Models.Service service = new Models.Service
            {
                Id = Guid.NewGuid(),
                Name = "ServiceName",
                Version = "1.0",
                ApiKey = "apiKey"
            };

            Factory factory = new Factory(FILE_SETTINGS_PATH);

            ApplicationService applicationService = (ApplicationService) factory.GetDbService(AvailableServices.TogglerApplication);
            Assert.NotNull(applicationService);
            applicationService.Insert(service);

            Models.Service dbService = applicationService.GetById(service.Id);
            Assert.NotNull(dbService);
            Assert.AreEqual(service.Id, dbService.Id);
            Assert.AreEqual(service.ApiKey, dbService.ApiKey);
            Assert.AreEqual(service.Name, dbService.Name);
            Assert.AreEqual(service.Version, dbService.Version);
        }

        [Test, Order(2)]
        public void ShouldInsertToggleWithAssociatedService()
        {
            Models.Service service = new Models.Service
            {
                Name = "ServiceName",
                Version = "1.0",
                ApiKey = "apiKey"
            };

            Factory factory = new Factory(FILE_SETTINGS_PATH);

            ApplicationService applicationService = (ApplicationService)factory.GetDbService(AvailableServices.TogglerApplication);
            Assert.NotNull(applicationService);

            service = applicationService.GetByExpression(x => x.Name == service.Name && x.Version == service.Version);
            Assert.NotNull(service);

            Toggle toggle = new Toggle
            {
                Id = Guid.NewGuid(),
                Value = true,
                ServiceList = new List<Models.Service>
                {
                    service
                },
                Name = "toggleName",
                Overrides = false
            };

            TogglerService togglerService = (TogglerService) factory.GetDbService(AvailableServices.Toggler);
            Assert.NotNull(togglerService);
            togglerService.Insert(toggle);

            Toggle dbToggle = togglerService.GetById(toggle.Id);
            Assert.NotNull(dbToggle);
            Assert.AreEqual(toggle.Id, dbToggle.Id);
            Assert.AreEqual(toggle.Name, dbToggle.Name);
            Assert.AreEqual(toggle.Overrides, dbToggle.Overrides);
            Assert.AreEqual(toggle.Value, dbToggle.Value);
            Assert.NotNull(dbToggle.ServiceList);
            Assert.AreEqual(toggle.ServiceList.Count, dbToggle.ServiceList.Count);
            Assert.IsNotEmpty(toggle.ServiceList);
        }

        [Test, Order(3)]
        public void ShouldUpdateToggleWhenInsertingAgain()
        {
            Models.Service service = new Models.Service
            {
                Name = "ServiceName",
                Version = "1.0",
                ApiKey = "apiKey"
            };

            Factory factory = new Factory(FILE_SETTINGS_PATH);

            ApplicationService applicationService = (ApplicationService)factory.GetDbService(AvailableServices.TogglerApplication);
            Assert.NotNull(applicationService);

            service = applicationService.GetByExpression(x => x.Name == service.Name && x.Version == service.Version);
            Assert.NotNull(service);

            TogglerService togglerService = (TogglerService)factory.GetDbService(AvailableServices.Toggler);
            Assert.NotNull(togglerService);

            Toggle toggle = togglerService.GetByExpression(x => x.Name == "toggleName" && x.Value == true);
            Assert.NotNull(toggle);

            togglerService.Insert(toggle);
            var toggleList = togglerService.GetAll()?.ToList();
            Assert.NotNull(toggleList);
            Assert.IsNotEmpty(toggleList);

            var matchingToggles = toggleList.Where(x => x.Name == "toggleName" && x.Value == true);
            Assert.NotNull(matchingToggles);
            Assert.AreEqual(1, matchingToggles.Count());
        }

        [Test, Order(4)]
        public void Should_RemoveServiceFromToggleWhenRemovingService()
        {
            Models.Service service = new Models.Service
            {
                Name = "ServiceName",
                Version = "1.0",
                ApiKey = "apiKey"
            };

            Factory factory = new Factory(FILE_SETTINGS_PATH);

            ApplicationService applicationService = (ApplicationService) factory.GetDbService(AvailableServices.TogglerApplication);
            Assert.NotNull(applicationService);

            service = applicationService.GetByExpression(x => x.Name == service.Name && x.Version == service.Version);
            Assert.NotNull(service);

            applicationService.Delete(service.Id);
            var dbService = applicationService.GetById(service.Id);
            Assert.IsNull(dbService);

            TogglerService togglerService = (TogglerService) factory.GetDbService(AvailableServices.Toggler);
            Assert.NotNull(togglerService);

            Toggle toggle = togglerService.GetByExpression(x => x.Name == "toggleName" && x.Value == true);
            Assert.NotNull(toggle);

            var serviceList = toggle.ServiceList;
            var matchingServices = serviceList.Where(x => x.Name == service.Name && x.Version == service.Version);

            Assert.NotNull(matchingServices);
            Assert.IsEmpty(matchingServices);
        }

        [Test]
        public void Should_RetriveAlwaysRetrieveOverrideToggle()
        {
            Models.Service service = new Models.Service
            {
                Id = Guid.NewGuid(),
                Name = "ServiceNameOverrideTest",
                Version = "1.0",
                ApiKey = "apiKey"
            };

            Factory factory = new Factory(FILE_SETTINGS_PATH);

            ApplicationService applicationService = (ApplicationService)factory.GetDbService(AvailableServices.TogglerApplication);
            Assert.NotNull(applicationService);
            applicationService.Insert(service);

            Models.Service dbService = applicationService.GetById(service.Id);
            Assert.NotNull(dbService);
            Assert.AreEqual(service.Id, dbService.Id);
            Assert.AreEqual(service.ApiKey, dbService.ApiKey);
            Assert.AreEqual(service.Name, dbService.Name);
            Assert.AreEqual(service.Version, dbService.Version);

            Toggle toggle1 = new Toggle
            {
                Id = Guid.NewGuid(),
                Value = true,
                ServiceList = new List<Models.Service>
                {
                    service
                },
                Name = "toggleName",
                Overrides = false
            };

            TogglerService togglerService = (TogglerService)factory.GetDbService(AvailableServices.Toggler);
            Assert.NotNull(togglerService);
            togglerService.Insert(toggle1);

            Toggle toggle2 = new Toggle
            {
                Id = Guid.NewGuid(),
                Value = false,
                ServiceList = new List<Models.Service>
                {
                    service
                },
                Name = "toggleName",
                Overrides = true
            };

            togglerService.Insert(toggle2);

            bool shouldNotExecute = togglerService.ShouldApplicationExecute(toggle1);
            Assert.IsFalse(shouldNotExecute);

            bool shouldExecute = togglerService.ShouldApplicationExecute(toggle2);
            Assert.IsTrue(shouldExecute);
        }

        [Test]
        public void Should_RetriveToggleValueForService()
        {
            Models.Service service = new Models.Service
            {
                Id = Guid.NewGuid(),
                Name = "ServiceNameTest",
                Version = "1.0",
                ApiKey = "apiKey"
            };

            Factory factory = new Factory(FILE_SETTINGS_PATH);

            ApplicationService applicationService = (ApplicationService)factory.GetDbService(AvailableServices.TogglerApplication);
            Assert.NotNull(applicationService);
            applicationService.Insert(service);

            Models.Service dbService = applicationService.GetById(service.Id);
            Assert.NotNull(dbService);
            Assert.AreEqual(service.Id, dbService.Id);
            Assert.AreEqual(service.ApiKey, dbService.ApiKey);
            Assert.AreEqual(service.Name, dbService.Name);
            Assert.AreEqual(service.Version, dbService.Version);

            Toggle toggle1 = new Toggle
            {
                Id = Guid.NewGuid(),
                Value = true,
                ServiceList = new List<Models.Service>
                {
                    service
                },
                Name = "newToggleName",
                Overrides = false
            };

            TogglerService togglerService = (TogglerService)factory.GetDbService(AvailableServices.Toggler);
            Assert.NotNull(togglerService);
            togglerService.Insert(toggle1);

            bool shouldExecute = togglerService.ShouldApplicationExecute(toggle1);
            Assert.IsTrue(shouldExecute);
        }

        [Test]
        public void Should_ThrowExceptionWhenNoServices()
        {
            Factory factory = new Factory(FILE_SETTINGS_PATH);
            Toggle toggle1 = new Toggle
            {
                Id = Guid.NewGuid(),
                Value = true,
                ServiceList = new List<Models.Service>
                {
                },
                Name = "toggleName",
                Overrides = false
            };

            TogglerService togglerService = (TogglerService)factory.GetDbService(AvailableServices.Toggler);
            Assert.NotNull(togglerService);
            togglerService.Insert(toggle1);

            try
            {
                bool shouldNotExecute = togglerService.ShouldApplicationExecute(toggle1);
                Assert.Fail();
            }
            catch (Exception)
            {
                Assert.Pass();
                throw;
            }
        }
    }
}
