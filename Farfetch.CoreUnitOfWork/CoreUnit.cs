using System;
using Farfetch.DB.MongoDB;
using Farfetch.Models;
using Farfetch.Repositories;
using Farfetch.Repositories.MongoDB;
using MongoDB.Driver;

namespace Farfetch.CoreUnitOfWork
{
    public class CoreUnit<T> where T : DbT
    {
        private Settings _settings;

        public IRepository<T> Repository { get; set; }

        public CoreUnit()
        {
            _settings = Settings.Instance;
            InitDatabase();
        }

        public void InitDatabase()
        {
            if(_settings == null) throw new NullReferenceException("Settings haven't been defined");
            if(_settings.DbSettings == null) throw new NullReferenceException("DbSettings haven't been defined");
            switch (_settings.DbSettings.Database)
            {
                case "MongoDB":
                    MongoDatabase<IMongoDatabase> database = new MongoDatabase<IMongoDatabase>();
                    database.Init(_settings.DbSettings);
                    Repository = new MongoRepository<T>(database);
                    break;
                default:
                    throw new NullReferenceException();
            }
        }
    }
}
