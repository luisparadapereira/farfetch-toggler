using System.Collections.Generic;
using System.Linq;
using Farfetch.Common;

namespace Farfetch.APIHandler.Common
{
    public class ApiKey
    {
        public string ServiceName { get; set; }
        public string ServiceVersion { get; set; }
        public string Key { get; set; }
        public string ApiName { get; set; }
    }

    public class ApiAssembly: AssemblyInformation
    {
        protected const string apiKeyPath = @"APIKEYS.json";

        /// <summary>
        /// The Api key
        /// </summary>
        protected ApiKey Key;

        public void GetApiKey(string serviceName, string serviceVersion)
        {
            SettingsReader<List<ApiKey>> reader = new SettingsReader<List<ApiKey>>();
            Key = reader.Read(apiKeyPath).FirstOrDefault(x => x.ServiceName == serviceName && x.ServiceVersion == serviceVersion);
        }
    }
}