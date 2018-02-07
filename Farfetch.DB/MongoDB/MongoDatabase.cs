using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Farfetch.DB.MongoDB
{
    public class MongoDatabase<T>: BaseDatabase, IDatabase<T>
    {
        private MongoClient _client;
        private DbSettings _settings;

        /// <inheritdoc />
        public void Init(DbSettings settings)
        {
            _settings = settings;
            string connectionString = BuildConnectionString(settings);
            _client = new MongoClient(connectionString);
        }

        /// <inheritdoc />
        public T GetDatabase()
        {
            if (_client == null) throw new NullReferenceException("Mongo client hasn't been defined");
            if (_settings == null) throw new NullReferenceException("Mongo settings haven't been defined");
            return (T) _client.GetDatabase(_settings.DatabaseName);
        }

        /// <inheritdoc />
        internal override string BuildConnectionString(DbSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));
            if (string.IsNullOrEmpty(settings.Database)) throw new NullReferenceException("Database not defined");
            if (string.IsNullOrEmpty(settings.Server)) throw new NullReferenceException("Server not defined");
            if (string.IsNullOrEmpty(settings.Port)) throw new NullReferenceException("Port not defined");

            // mongodb://[username:password@]hostname[:port][/[database][?options]]
            string connectionString = settings.Server + ":" + settings.Port;
            if (!string.IsNullOrEmpty(settings.Username) && !string.IsNullOrEmpty(settings.Password))
            {
                connectionString = settings.Username + ":" + settings.Password + "@";
            }

            connectionString = "mongodb://" + connectionString;

            return connectionString;
        }
    }
}