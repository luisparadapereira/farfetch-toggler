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
    public class TogglerControllerTests: TestBase
    {
        [Test]
        public void Should_GetAll()
        {
            IConfiguration config = InitConfiguration();
            TogglerController togglerController = new TogglerController(config);

            var farfetchMessage = togglerController.GetAll();
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<IEnumerable<ToggleListDto>>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.IsNotEmpty(farfetchMessage.Result);
            Assert.AreEqual(typeof(ToggleListDto), farfetchMessage.Result.FirstOrDefault().GetType());
        }

        [Test]
        public void Should_Insert()
        {
            IConfiguration config = InitConfiguration();
            TogglerController togglerController = new TogglerController(config);

            ToggleDto toggle = new ToggleDto
            {
                Id = Guid.Empty,
                Name = "ToggleNameTestInsert",
                ServiceList = new List<ServiceDto>(),
                Value = true,
                Overrides = false
            };

            var farfetchMessage = togglerController.Insert(toggle);
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<ToggleDto>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.AreEqual(toggle.Name, farfetchMessage.Result.Name);
            Assert.AreEqual(toggle.Value, farfetchMessage.Result.Value);
            Assert.AreEqual(toggle.Overrides, farfetchMessage.Result.Overrides);
            Assert.NotNull(farfetchMessage.Result.ServiceList);
            Assert.IsEmpty(farfetchMessage.Result.ServiceList);
        }

        [Test]
        public void Should_Get()
        {
            IConfiguration config = InitConfiguration();
            TogglerController togglerController = new TogglerController(config);

            ToggleDto toggle = new ToggleDto
            {
                Id = Guid.Empty,
                Name = "ToggleNameTestGet",
                ServiceList = new List<ServiceDto>(),
                Value = true,
                Overrides = false
            };

            var farfetchMessage = togglerController.Insert(toggle);
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<ToggleDto>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.AreEqual(toggle.Name, farfetchMessage.Result.Name);
            Assert.AreEqual(toggle.Value, farfetchMessage.Result.Value);
            Assert.AreEqual(toggle.Overrides, farfetchMessage.Result.Overrides);
            Assert.NotNull(farfetchMessage.Result.ServiceList);
            Assert.IsEmpty(farfetchMessage.Result.ServiceList);

            var farfetchMessage2 = togglerController.Get(farfetchMessage.Result.Id);
            Assert.NotNull(farfetchMessage2);
            Assert.AreEqual(typeof(FarfetchMessage<ToggleDto>), farfetchMessage2.GetType());
            Assert.NotNull(farfetchMessage2.Result);
            Assert.AreEqual(farfetchMessage.Result.Id, farfetchMessage2.Result.Id);
            Assert.AreEqual(toggle.Name, farfetchMessage2.Result.Name);
            Assert.AreEqual(toggle.Value, farfetchMessage2.Result.Value);
            Assert.AreEqual(toggle.Overrides, farfetchMessage2.Result.Overrides);
            Assert.NotNull(farfetchMessage2.Result.ServiceList);
            Assert.IsEmpty(farfetchMessage2.Result.ServiceList);
        }

        [Test]
        public void Should_Update()
        {
            IConfiguration config = InitConfiguration();
            TogglerController togglerController = new TogglerController(config);

            ToggleDto toggle = new ToggleDto
            {
                Id = Guid.Empty,
                Name = "ToggleNameTestUpdate",
                ServiceList = new List<ServiceDto>(),
                Value = true,
                Overrides = false
            };

            var farfetchMessage = togglerController.Insert(toggle);
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<ToggleDto>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.AreEqual(toggle.Name, farfetchMessage.Result.Name);
            Assert.AreEqual(toggle.Value, farfetchMessage.Result.Value);
            Assert.AreEqual(toggle.Overrides, farfetchMessage.Result.Overrides);
            Assert.NotNull(farfetchMessage.Result.ServiceList);
            Assert.IsEmpty(farfetchMessage.Result.ServiceList);

            farfetchMessage.Result.Value = false;

            var farfetchMessage2 = togglerController.Update(farfetchMessage.Result);
            Assert.NotNull(farfetchMessage2);
            Assert.AreEqual(typeof(FarfetchMessage<ToggleDto>), farfetchMessage2.GetType());
            Assert.NotNull(farfetchMessage2.Result);
            Assert.AreEqual(farfetchMessage.Result.Id, farfetchMessage2.Result.Id);
            Assert.AreEqual(toggle.Name, farfetchMessage2.Result.Name);
            Assert.AreEqual(false, farfetchMessage2.Result.Value);
            Assert.AreEqual(toggle.Overrides, farfetchMessage2.Result.Overrides);
            Assert.NotNull(farfetchMessage2.Result.ServiceList);
            Assert.IsEmpty(farfetchMessage2.Result.ServiceList);

            togglerController.Delete(farfetchMessage2.Result.Id);
        }

        [Test]
        public void Should_GetForService()
        {
            IConfiguration config = InitConfiguration();
            TogglerController togglerController = new TogglerController(config);

            ToggleDto toggle = new ToggleDto
            {
                Id = Guid.Empty,
                Name = "ToggleNameTestUpdate",
                ServiceList = new List<ServiceDto>()
                {
                    new ServiceDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "ServiceTestIntegration",
                        Version = "1.0",
                        ApiKey = "apiKey"
                    }
                },
                Value = true,
                Overrides = false
            };

            var farfetchMessage = togglerController.Insert(toggle);
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<ToggleDto>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.AreEqual(toggle.Name, farfetchMessage.Result.Name);
            Assert.AreEqual(toggle.Value, farfetchMessage.Result.Value);
            Assert.AreEqual(toggle.Overrides, farfetchMessage.Result.Overrides);
            Assert.NotNull(farfetchMessage.Result.ServiceList);
            Assert.IsNotEmpty(farfetchMessage.Result.ServiceList);

            farfetchMessage.Result.Value = false;

            var farfetchMessage2 = togglerController.GetForService(toggle.Name, toggle.Value, toggle.ServiceList.FirstOrDefault().Name,
                toggle.ServiceList.FirstOrDefault().Version);
            Assert.NotNull(farfetchMessage2);
            Assert.AreEqual(typeof(FarfetchMessage<bool>), farfetchMessage2.GetType());
            Assert.NotNull(farfetchMessage2.Result);
            Assert.AreEqual(true, farfetchMessage2.Result);
        }

        [Test]
        public void Should_Delete()
        {
            IConfiguration config = InitConfiguration();
            TogglerController togglerController = new TogglerController(config);

            ToggleDto toggle = new ToggleDto
            {
                Id = Guid.Empty,
                Name = "ToggleNameTestDelete",
                ServiceList = new List<ServiceDto>(),
                Value = true,
                Overrides = false
            };

            var farfetchMessage = togglerController.Insert(toggle);
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<ToggleDto>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.AreEqual(toggle.Name, farfetchMessage.Result.Name);
            Assert.AreEqual(toggle.Value, farfetchMessage.Result.Value);
            Assert.AreEqual(toggle.Overrides, farfetchMessage.Result.Overrides);
            Assert.NotNull(farfetchMessage.Result.ServiceList);
            Assert.IsEmpty(farfetchMessage.Result.ServiceList);

            farfetchMessage.Result.Value = false;

            var farfetchMessage2 = togglerController.Delete(farfetchMessage.Result.Id);
            Assert.NotNull(farfetchMessage2);
            Assert.AreEqual(typeof(FarfetchMessage<bool>), farfetchMessage2.GetType());
            Assert.NotNull(farfetchMessage2.Result);
            Assert.AreEqual(true, farfetchMessage2.Result);

            var farfetchMessage3 = togglerController.Get(farfetchMessage.Result.Id);
            Assert.NotNull(farfetchMessage3);
            Assert.AreEqual(typeof(FarfetchMessage<ToggleDto>), farfetchMessage.GetType());
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
