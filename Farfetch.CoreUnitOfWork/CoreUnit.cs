﻿using System;
using Farfetch.Common;
using Farfetch.DB;
using Farfetch.DB.MongoDB;
using Farfetch.Models;
using Farfetch.Repositories;
using Farfetch.Repositories.MongoDB;
using MongoDB.Driver;

namespace Farfetch.CoreUnitOfWork
{
    public class CoreUnit<T> where T : DbT
    {
        private const string SETTINGS_FILEPATH = @"dbsettings.json";

        private readonly DbSettings _dbSettings;

        public IRepository<T> Repository { get; private set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public CoreUnit()
        {
            SettingsReader<DbSettings> reader = new SettingsReader<DbSettings>();
            _dbSettings = reader.Read(SETTINGS_FILEPATH);
            InitDatabase();
        }

        /// <summary>
        /// Initializes the database
        /// </summary>
        private void InitDatabase()
        {
            if(_dbSettings == null) throw new NullReferenceException("DbSettings haven't been defined");
            switch (_dbSettings.Database)
            {
                case "MongoDB":
                    IDatabase<IMongoDatabase> database = new MongoDatabase<IMongoDatabase>();
                    database.Init(_dbSettings);
                    Repository = new MongoRepository<T>(database);
                    break;
                default:
                    throw new NullReferenceException();
            }
        }
    }
}
