using System.IO;
using Farfetch.DB;
using Newtonsoft.Json;

namespace Farfetch.CoreUnitOfWork
{
    public sealed class Settings
    {
        private const string SETTINGS_FILEPATH = @"dbsettings.json";

        public DbSettings DbSettings { get;}

        private static readonly Settings currentInstance = new Settings();

        static Settings()
        {

        }

        private Settings()
        {
            using (StreamReader file = File.OpenText(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, SETTINGS_FILEPATH)))
            {
                JsonSerializer serializer = new JsonSerializer();
                DbSettings = (DbSettings)serializer.Deserialize(file, typeof(DbSettings));
            }
        }

        public static Settings Instance => currentInstance;
    }
}