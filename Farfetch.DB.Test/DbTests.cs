using Farfetch.Common;
using Farfetch.DB.MongoDB;
using Farfetch.Tests;
using MongoDB.Driver;
using NUnit.Framework;

namespace Farfetch.DB.Test
{
    [TestFixture]
    public class DbTests: TestBase
    {
        [Test]
        public void Should_GetMongoDatabase()
        {
            SettingsReader<DbSettings> reader = new SettingsReader<DbSettings>();
            DbSettings settings = reader.Read(@"dbsettings_test.json");

            IDatabase<IMongoDatabase> database = new MongoDatabase<IMongoDatabase>();
            database.Init(settings);

            Assert.NotNull(database);

            IMongoDatabase db = database.GetDatabase();
            Assert.NotNull(db);
            Assert.NotNull(db.Client);
            Assert.NotNull(db.DatabaseNamespace);
            Assert.NotNull(db.Settings);
        }
    }
}
