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
        [Test]
        public void Should_InitializeTheRepository_ForUsers()
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
        }
    }
}
