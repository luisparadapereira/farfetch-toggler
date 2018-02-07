using System;
using MongoDB.Driver;

namespace Farfetch.DB.MongoDB
{
    public class MongoDatabase<T>: IDatabase<T>
    {
        private MongoClient _client;
        private DbSettings _settings;
        private string _connectionString;

        /// <inheritdoc />
        public void Init(DbSettings settings)
        {
            _settings = settings;
            BuildConnectionString();
            _client = new MongoClient(_connectionString);
        }

        /// <inheritdoc />
        public T GetDatabase()
        {
            if (_client == null) throw new NullReferenceException("Mongo client hasn't been defined");
            if (_settings == null) throw new NullReferenceException("Mongo settings haven't been defined");
            return (T) _client.GetDatabase(_settings.DatabaseName);
        }

        /// <inheritdoc />
        /// <remarks>
        /// Mongo Specific Connection String: mongodb://[username:password@]hostname[:port][/[database][?options]]
        /// </remarks>
        public void BuildConnectionString()
        {
            if (_settings == null) throw new NullReferenceException("Settings are not defined");
            if (string.IsNullOrEmpty(_settings.Database)) throw new NullReferenceException("Database not defined");
            if (string.IsNullOrEmpty(_settings.Server)) throw new NullReferenceException("Server not defined");
            if (string.IsNullOrEmpty(_settings.Port)) throw new NullReferenceException("Port not defined");

            _connectionString = _settings.Server + ":" + _settings.Port;
            if (!string.IsNullOrEmpty(_settings.Username) && !string.IsNullOrEmpty(_settings.Password))
            {
                _connectionString = _settings.Username + ":" + _settings.Password + "@";
            }

            _connectionString = "mongodb://" + _connectionString;
        }
    }
}