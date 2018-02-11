using System;
using System.IO;
using Newtonsoft.Json;

namespace Farfetch.Common
{
    public class SettingsReader<T>
    {
        /// <summary>
        /// Reads a file and serializes the information into type T
        /// </summary>
        /// <param name="filePath">The path of the settings file</param>
        /// <returns>The serialized settings</returns>
        public T Read(string filePath)
        {
            if (string.IsNullOrEmpty(filePath)) throw new ArgumentNullException(nameof(filePath));

            T settings;
            string localFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filePath);
            if (!File.Exists(localFilePath)) throw new FileNotFoundException();

            using (StreamReader file = File.OpenText(localFilePath))
            {
                JsonSerializer serializer = new JsonSerializer();
                settings = (T)serializer.Deserialize(file, typeof(T));
            }

            return settings;
        }
    }
}
