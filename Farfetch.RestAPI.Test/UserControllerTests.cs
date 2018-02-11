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
    public class UserControllerTests : TestBase
    {
        [Test, Order(2)]
        public void Should_GetAll()
        {
            IConfiguration config = InitConfiguration();
            UserAccountsController userAccountsController = new UserAccountsController(config);

            var farfetchMessage = userAccountsController.GetAll();
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<IEnumerable<UserLoginDto>>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.IsNotEmpty(farfetchMessage.Result);
            Assert.AreEqual(typeof(UserLoginDto), farfetchMessage.Result.FirstOrDefault().GetType());
        }

        [Test, Order(1)]
        public void Should_Insert()
        {
            IConfiguration config = InitConfiguration();
            UserAccountsController userAccountsController = new UserAccountsController(config);

            UserLoginDto user = new UserLoginDto
            {
                Username = "usernameTestInsert",
                Password = "passwordTest",
                Profile = "Public"
            };

            var farfetchMessage = userAccountsController.Insert(user);
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<UserLoginDto>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.AreEqual(user.Username, farfetchMessage.Result.Username);
            Assert.AreEqual(user.Password, farfetchMessage.Result.Password);
            Assert.AreEqual(user.Profile, farfetchMessage.Result.Profile);
        }

        [Test, Order(3)]
        public void Should_Update()
        {
            IConfiguration config = InitConfiguration();
            UserAccountsController userAccountsController = new UserAccountsController(config);

            UserLoginDto user = new UserLoginDto
            {
                Username = "usernameTestUpdate",
                Password = "passwordTest",
                Profile = "Public"
            };

            var farfetchMessage = userAccountsController.Insert(user);
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<UserLoginDto>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.AreEqual(user.Username, farfetchMessage.Result.Username);
            Assert.AreEqual(user.Password, farfetchMessage.Result.Password);
            Assert.AreEqual(user.Profile, farfetchMessage.Result.Profile);

            farfetchMessage.Result.Profile = "profile updated";

            var farfetchMessage2 = userAccountsController.Update(farfetchMessage.Result);
            Assert.NotNull(farfetchMessage2);
            Assert.AreEqual(typeof(FarfetchMessage<UserLoginDto>), farfetchMessage2.GetType());
            Assert.NotNull(farfetchMessage2.Result);
            Assert.AreEqual(user.Username, farfetchMessage2.Result.Username);
            Assert.AreEqual(user.Password, farfetchMessage2.Result.Password);
            Assert.AreEqual("profile updated", farfetchMessage2.Result.Profile);

            userAccountsController.Delete(farfetchMessage2.Result.Username);
        }

        [Test, Order(4)]
        public void Should_Delete()
        {
            IConfiguration config = InitConfiguration();
            UserAccountsController userAccountsController = new UserAccountsController(config);

            UserLoginDto user = new UserLoginDto
            {
                Username = "usernameTestDelete",
                Password = "passwordTest",
                Profile = "Public"
            };

            var farfetchMessage = userAccountsController.Insert(user);
            Assert.NotNull(farfetchMessage);
            Assert.AreEqual(typeof(FarfetchMessage<UserLoginDto>), farfetchMessage.GetType());
            Assert.NotNull(farfetchMessage.Result);
            Assert.AreEqual(user.Username, farfetchMessage.Result.Username);
            Assert.AreEqual(user.Password, farfetchMessage.Result.Password);
            Assert.AreEqual(user.Profile, farfetchMessage.Result.Profile);

            var farfetchMessage2 = userAccountsController.Delete(farfetchMessage.Result.Username);
            Assert.NotNull(farfetchMessage2);
            Assert.AreEqual(typeof(FarfetchMessage<bool>), farfetchMessage2.GetType());
            Assert.NotNull(farfetchMessage2.Result);
            Assert.AreEqual(true, farfetchMessage2.Result);
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
