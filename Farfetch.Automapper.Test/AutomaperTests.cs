using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Farfetch.APIHandler.Common.DTO;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Farfetch.Automapper.Profiles.Authentication;
using Farfetch.Automapper.Profiles.TogglerAPI;
using Farfetch.Models;
using Farfetch.Tests;
using NUnit.Framework;

namespace Farfetch.Automapper.Test
{
    [TestFixture]
    public class AutomaperTests: TestBase
    {
        [Test]
        public void Should_HaveCorrectMappings_UserProfile()
        {
            var mapperConfig = new MapperConfiguration(
                cfg =>
                {
                    cfg?.AddProfile<UserAMProfile>();
                });
            mapperConfig.AssertConfigurationIsValid();
        }

        [Test]
        public void Should_HaveCorrectMappings_ServiceProfile()
        {
            var mapperConfig = new MapperConfiguration(
                cfg =>
                {
                    cfg?.AddProfile<ServiceAMProfile>();
                });
            mapperConfig.AssertConfigurationIsValid();
        }

        [Test]
        public void Should_HaveCorrectMappings_ToggleProfile()
        {
            var mapperConfig = new MapperConfiguration(
                cfg =>
                {
                    cfg?.AddProfile<ToggleAMProfile>();
                });
            mapperConfig.AssertConfigurationIsValid();
        }

        [Test]
        public void Should_MapUserToUserLoginDto()
        {
            User user = new User
            {
                Id = Guid.NewGuid(),
                Username = "testUsername",
                Password = "testPassword",
                Profile = "testProfile"
            };

            UserLoginDto userDto = Mapper.Map<User, UserLoginDto>(user);

            Assert.NotNull(userDto);
            Assert.AreEqual(user.Username, userDto.Username);
            Assert.AreEqual(user.Password, userDto.Password);
            Assert.AreEqual(user.Profile, userDto.Profile);
        }

        [Test]
        public void Should_MapUserDtoToUser()
        {
            UserLoginDto userDto = new UserLoginDto
            {
                Username = "testUsername",
                Password = "testPassword",
                Profile = "testProfile"
            };

            User user = Mapper.Map<UserLoginDto, User>(userDto);

            Assert.NotNull(user);
            Assert.AreEqual(Guid.Empty, user.Id);
            Assert.AreEqual(userDto.Username, user.Username);
            Assert.AreEqual(userDto.Password, user.Password);
            Assert.AreEqual(userDto.Profile, user.Profile);
        }

        [Test]
        public void Should_MapServiceToServiceDto()
        {
            Service service = new Service
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Version = "testVersion",
                ApiKey = "testApiKey"
            };

            ServiceDto serviceDto = Mapper.Map<Service, ServiceDto>(service);

            Assert.NotNull(serviceDto);
            Assert.AreEqual(service.Id, serviceDto.Id);
            Assert.AreEqual(service.Name, serviceDto.Name);
            Assert.AreEqual(service.Version, serviceDto.Version);
            Assert.AreEqual(service.ApiKey, serviceDto.ApiKey);

        }

        [Test]
        public void Should_MapServiceDtoToService()
        {
            ServiceDto serviceDto = new ServiceDto
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Version = "testVersion",
                ApiKey = "testApiKey"
            };

            Service service = Mapper.Map<ServiceDto, Service>(serviceDto);

            Assert.NotNull(service);
            Assert.AreEqual(serviceDto.Id, service.Id);
            Assert.AreEqual(serviceDto.Name, service.Name);
            Assert.AreEqual(serviceDto.Version, service.Version);
            Assert.AreEqual(serviceDto.ApiKey, service.ApiKey);
        }

        [Test]
        public void Should_MapToggleToToggleDto()
        {
            Toggle toggle = new Toggle
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Value = true,
                Overrides = true,
                ServiceList = new List<Service>
                {
                    new Service
                    {
                        Id = Guid.NewGuid(),
                        Name = "testName",
                        Version = "testVersion",
                        ApiKey = "testApiKey"
                    }
                }
            };

            ToggleDto toggleDto = Mapper.Map<Toggle, ToggleDto>(toggle);

            Assert.NotNull(toggleDto);
            Assert.AreEqual(toggle.Id, toggleDto.Id);
            Assert.AreEqual(toggle.Name, toggleDto.Name);
            Assert.AreEqual(toggle.Value, toggleDto.Value);
            Assert.AreEqual(toggle.Overrides, toggleDto.Overrides);
            Assert.NotNull(toggle.ServiceList);
            Assert.AreEqual(toggle.ServiceList.FirstOrDefault()?.Id, toggleDto.ServiceList.FirstOrDefault()?.Id);
            Assert.AreEqual(toggle.ServiceList.FirstOrDefault()?.Name, toggleDto.ServiceList.FirstOrDefault()?.Name);
            Assert.AreEqual(toggle.ServiceList.FirstOrDefault()?.Version, toggleDto.ServiceList.FirstOrDefault()?.Version);
            Assert.AreEqual(toggle.ServiceList.FirstOrDefault()?.ApiKey, toggleDto.ServiceList.FirstOrDefault()?.ApiKey);
        }

        [Test]
        public void Should_MapToggleDtoToToggle()
        {
            ToggleDto toggleDto = new ToggleDto
            {
                Id = Guid.NewGuid(),
                Name = "testName",
                Value = true,
                Overrides = true,
                ServiceList = new List<ServiceDto>
                {
                    new ServiceDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "testName",
                        Version = "testVersion",
                        ApiKey = "testApiKey"
                    }
                }
            };

            Toggle toggle = Mapper.Map<ToggleDto, Toggle>(toggleDto);

            Assert.NotNull(toggle);
            Assert.AreEqual(toggleDto.Id, toggle.Id);
            Assert.AreEqual(toggleDto.Name, toggle.Name);
            Assert.AreEqual(toggleDto.Value, toggle.Value);
            Assert.AreEqual(toggleDto.Overrides, toggle.Overrides);
            Assert.NotNull(toggleDto.ServiceList);
            Assert.AreEqual(toggleDto.ServiceList.FirstOrDefault()?.Id, toggle.ServiceList.FirstOrDefault()?.Id);
            Assert.AreEqual(toggleDto.ServiceList.FirstOrDefault()?.Name, toggle.ServiceList.FirstOrDefault()?.Name);
            Assert.AreEqual(toggleDto.ServiceList.FirstOrDefault()?.Version, toggle.ServiceList.FirstOrDefault()?.Version);
            Assert.AreEqual(toggleDto.ServiceList.FirstOrDefault()?.ApiKey, toggle.ServiceList.FirstOrDefault()?.ApiKey);
        }
    }
}


