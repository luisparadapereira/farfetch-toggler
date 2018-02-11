using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Farfetch.APIHandler.Common.DTO;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Farfetch.RestAPI.Controllers;
using Farfetch.Tests;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace Farfetch.RestAPI.Test
{
    [TestFixture]
    public class ServiceControllerTests : TestBase
    {
        [Test]
        public void Should_GetAll()
        {
            IConfiguration config = InitConfiguration();
            ServiceController serviceController = new ServiceController(config);

            var farfetchMessage = serviceController.GetAll();
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<IEnumerable<ServiceDto>>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.IsNotEmpty(farfetchMessage.Result);
            Assert.AreEqual(typeof(ServiceDto), farfetchMessage.Result.FirstOrDefault().GetType());
        }

        [Test]
        public void Should_Insert()
        {
            IConfiguration config = InitConfiguration();
            ServiceController serviceController = new ServiceController(config);

            ServiceDto service = new ServiceDto
            {
                Id = Guid.Empty,
                Name = "ServiceNameTestInsert",
                Version = "ServiceVersion",
                ApiKey = "apiKeyTest"
            };

            var farfetchMessage = serviceController.Insert(service);
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<ServiceDto>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.AreEqual(service.Name, farfetchMessage.Result.Name);
            Assert.AreEqual(service.Version, farfetchMessage.Result.Version);
            Assert.AreEqual(service.ApiKey, farfetchMessage.Result.ApiKey);
        }

        [Test]
        public void Should_Get()
        {
            IConfiguration config = InitConfiguration();
            ServiceController serviceController = new ServiceController(config);

            ServiceDto service = new ServiceDto
            {
                Id = Guid.Empty,
                Name = "ServiceNameTestGet",
                Version = "ServiceVersion",
                ApiKey = "apiKeyTest"
            };

            var farfetchMessage = serviceController.Insert(service);
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<ServiceDto>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.AreEqual(service.Name, farfetchMessage.Result.Name);
            Assert.AreEqual(service.Version, farfetchMessage.Result.Version);
            Assert.AreEqual(service.ApiKey, farfetchMessage.Result.ApiKey);

            var farfetchMessage2 = serviceController.Get(farfetchMessage.Result.Id);
            Assert.NotNull(farfetchMessage2);
            Assert.AreEqual(typeof(FarfetchMessage<ServiceDto>), farfetchMessage2.GetType());
            Assert.NotNull(farfetchMessage2.Result);
            Assert.AreEqual(farfetchMessage.Result.Id, farfetchMessage2.Result.Id);
            Assert.AreEqual(service.Name, farfetchMessage2.Result.Name);
            Assert.AreEqual(service.Version, farfetchMessage2.Result.Version);
            Assert.AreEqual(service.ApiKey, farfetchMessage2.Result.ApiKey);
        }

        [Test]
        public void Should_Update()
        {
            IConfiguration config = InitConfiguration();
            ServiceController serviceController = new ServiceController(config);

            ServiceDto service = new ServiceDto
            {
                Id = Guid.Empty,
                Name = "ServiceNameTestUpdate",
                Version = "ServiceVersion",
                ApiKey = "apiKeyTest"
            };

            var farfetchMessage = serviceController.Insert(service);
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<ServiceDto>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.AreEqual(service.Name, farfetchMessage.Result.Name);
            Assert.AreEqual(service.Version, farfetchMessage.Result.Version);
            Assert.AreEqual(service.ApiKey, farfetchMessage.Result.ApiKey);

            farfetchMessage.Result.ApiKey = "api key updated";

            var farfetchMessage2 = serviceController.Update(farfetchMessage.Result);
            Assert.NotNull(farfetchMessage2);
            Assert.AreEqual(typeof(FarfetchMessage<ServiceDto>), farfetchMessage2.GetType());
            Assert.NotNull(farfetchMessage2.Result);
            Assert.AreEqual(farfetchMessage.Result.Id, farfetchMessage2.Result.Id);
            Assert.AreEqual(service.Name, farfetchMessage2.Result.Name);
            Assert.AreEqual(service.Version, farfetchMessage2.Result.Version);
            Assert.AreEqual("api key updated", farfetchMessage2.Result.ApiKey);

            serviceController.Delete(farfetchMessage2.Result.Id);
        }

        [Test]
        public void Should_Delete()
        {
            IConfiguration config = InitConfiguration();
            ServiceController serviceController = new ServiceController(config);

            ServiceDto service = new ServiceDto
            {
                Id = Guid.Empty,
                Name = "ServiceNameTestDelete",
                Version = "ServiceVersion",
                ApiKey = "apiKeyTest"
            };

            var farfetchMessage = serviceController.Insert(service);
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<ServiceDto>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.AreEqual(service.Name, farfetchMessage.Result.Name);
            Assert.AreEqual(service.Version, farfetchMessage.Result.Version);
            Assert.AreEqual(service.ApiKey, farfetchMessage.Result.ApiKey);

            var farfetchMessage2 = serviceController.Delete(farfetchMessage.Result.Id);
            Assert.NotNull(farfetchMessage2);
            Assert.AreEqual(typeof(FarfetchMessage<bool>), farfetchMessage2.GetType());
            Assert.NotNull(farfetchMessage2.Result);
            Assert.AreEqual(true, farfetchMessage2.Result);

            var farfetchMessage3 = serviceController.Get(farfetchMessage.Result.Id);
            Assert.NotNull(farfetchMessage3);
            Assert.AreEqual(typeof(FarfetchMessage<ServiceDto>), farfetchMessage.GetType());
            Assert.IsNull(farfetchMessage3.Result);
        }

        public static IConfiguration InitConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings_test.json");

            return builder.Build();

        }
    }
}
