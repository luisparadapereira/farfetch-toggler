using System;
using System.Collections.Generic;
using Farfetch.Common;
using Farfetch.DB;
using Farfetch.DB.MongoDB;
using Farfetch.Models;
using Farfetch.Repositories.MongoDB;
using Farfetch.Tests;
using MongoDB.Driver;
using NUnit.Framework;

namespace Farfetch.Repositories.Test
{
    [TestFixture]
    public class RepositoryTests: TestBase
    {
        [Test, Order(1)]
        public void Should_InitializeTheRepository_ForUsers()
        {
            IRepository<User> repository = StartRepository();
            Assert.IsNotNull(repository);
        }

        [Test, Order(2)]
        public void Should_InsertData()
        {
            IRepository<User> repository = StartRepository();
            Assert.IsNotNull(repository);

            User testUser1 = new User
            {
                Id = Guid.NewGuid(),
                Password = "testPassword",
                Profile = "testProfile",
                Username = "testUsername"
            };

            repository.Insert(testUser1);

            User testExistingUser = repository.GetSingle(x => x.Id == testUser1.Id);

            Assert.NotNull(testExistingUser);
            Assert.AreEqual(testUser1.Id, testExistingUser.Id);
            Assert.AreEqual(testUser1.Password, testExistingUser.Password);
            Assert.AreEqual(testUser1.Username, testExistingUser.Username);
            Assert.AreEqual(testUser1.Profile, testExistingUser.Profile);
        }

        [Test]
        public void Should_UpdateData()
        {
            IRepository<User> repository = StartRepository(); ;
            Assert.IsNotNull(repository);

            User testUser1 = new User
            {
                Id = Guid.NewGuid(),
                Password = "testPassword",
                Profile = "testProfile",
                Username = "testUsername"
            };

            repository.Insert(testUser1);

            User testExistingUser = repository.GetSingle(x => x.Id == testUser1.Id);
            Assert.NotNull(testExistingUser);

            string newProfile = "New Profile Description";
            testExistingUser.Profile = newProfile;

            repository.Update(testExistingUser);
            User updatedUser = repository.GetSingle(x => x.Id == testUser1.Id);

            Assert.NotNull(updatedUser);
            Assert.AreEqual(testUser1.Id, updatedUser.Id);
            Assert.AreEqual(newProfile, updatedUser.Profile);

            repository.Delete(x => x.Id == testExistingUser.Id);
        }

        [Test]
        public void Should_GetAllData()
        {
            IRepository<User> repository = StartRepository();
            Assert.IsNotNull(repository);

            User testUser1 = new User
            {
                Id = Guid.NewGuid(),
                Password = "testPassword",
                Profile = "testProfile",
                Username = "testUsername"
            };

            User testUser2 = new User
            {
                Id = Guid.NewGuid(),
                Password = "testPassword",
                Profile = "testProfile",
                Username = "testUsername"
            };

            repository.Insert(testUser1);
            repository.Insert(testUser2);

            List<User> userList = repository.GetAll();
            Assert.NotNull(userList);
            Assert.IsNotEmpty(userList);
            
            var containsUser1 = userList.Exists(x => 
                    x.Id == testUser1.Id &&
                    x.Username == testUser1.Username &&
                    x.Password == testUser1.Password &&
                    x.Profile == testUser1.Profile);
            Assert.IsTrue(containsUser1);

            var containsUser2 = userList.Exists(x =>
                    x.Id == testUser2.Id &&
                    x.Username == testUser2.Username &&
                    x.Password == testUser2.Password &&
                    x.Profile == testUser2.Profile);
            Assert.IsTrue(containsUser2);
        }

        [Test]
        public void Should_DeleteData()
        {
            IRepository<User> repository = StartRepository();
            Assert.IsNotNull(repository);

            User testUser1 = new User
            {
                Id = Guid.NewGuid(),
                Password = "testPassword",
                Profile = "testProfile",
                Username = "testUsername"
            };

            repository.Insert(testUser1);

            User existingUser = repository.GetSingle(x => x.Id == testUser1.Id);
            Assert.NotNull(existingUser);

            repository.Delete(x => x.Id == testUser1.Id);
            User shouldNotExist = repository.GetSingle(x => x.Id == testUser1.Id);
            Assert.IsNull(shouldNotExist);

        }

        [Test]
        public void Should_ClearTheEntireRepository()
        {
            SettingsReader<DbSettings> reader = new SettingsReader<DbSettings>();
            DbSettings settings = reader.Read(@"dbsettings_test.json");

            IDatabase<IMongoDatabase> database = new MongoDatabase<IMongoDatabase>();
            database.Init(settings);

            Assert.NotNull(database);

            IMongoDatabase db = database.GetDatabase();
            Assert.NotNull(db);
            Assert.NotNull(db.Client);
            Assert.NotNull(db.Settings);

            IRepository<User> repository = new MongoRepository<User>(database);
            Assert.IsNotNull(repository);

            List<User> allUsers = repository.GetAll();
            Assert.NotNull(allUsers);

            foreach (User currentUser in allUsers)
            {
                repository.Delete(x => x.Id == currentUser.Id);
            }

            List<User> allUsersShouldBeEmpty = repository.GetAll();
            Assert.NotNull(allUsersShouldBeEmpty);
            Assert.AreEqual(0, allUsersShouldBeEmpty.Count);


        }

        private IRepository<User> StartRepository()
        {
            SettingsReader<DbSettings> reader = new SettingsReader<DbSettings>();
            DbSettings settings = reader.Read(@"dbsettings_test.json");

            IDatabase<IMongoDatabase> database = new MongoDatabase<IMongoDatabase>();
            database.Init(settings);

            Assert.NotNull(database);

            IMongoDatabase db = database.GetDatabase();
            Assert.NotNull(db);
            Assert.NotNull(db.Client);
            Assert.NotNull(db.Settings);

            IRepository<User> repository = new MongoRepository<User>(database);
            return repository;
        }
    }
}
