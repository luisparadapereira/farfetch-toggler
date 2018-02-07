using System;
using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Farfetch.Common;

namespace Farfetch.APIHandler.TogglerAPI
{
    /// <inheritdoc />
    /// <summary>
    /// internally used class to handle all requests made by applications to the Toggler API
    /// </summary>
    public class TogglerApiInternal : ExternalApi
    {
        /// <summary>
        /// ApiCommunication to handle requests
        /// </summary>
        private readonly ApiCommunication _apiCommunication;

        /// <summary>
        /// Settings of the Api
        /// </summary>
        private readonly ApiSettings _settings;

        /// <summary>
        /// FilePath for the API settings
        /// </summary>
        private const string SETTINGS_FILEPATH = @"TogglerAPI/restSettings.json";

        /// <summary>
        /// Default constructor initializes communications and the settings
        /// </summary>
        public TogglerApiInternal()
        {
            _apiCommunication = new ApiCommunication();
            SettingsReader<ApiSettings> reader = new SettingsReader<ApiSettings>();
            _settings = reader.Read(SETTINGS_FILEPATH);
        }

        /// <summary>
        /// Given toggle and service information this class will define what route the application should follow
        /// </summary>
        /// <param name="toggleName">The name of the toggle</param>
        /// <param name="toggleValue">The value of the toggle</param>
        /// <param name="applicationName">The name of the application/service</param>
        /// <param name="applicationVersion">The version of the application/service</param>
        /// <returns>Boolean specifying if the operation should be executed or not</returns>
        public bool CheckToggle(string toggleName, bool toggleValue, string applicationName, string applicationVersion)
        {
            if (string.IsNullOrEmpty(toggleName)) throw new ArgumentNullException(nameof(toggleName));
            if (string.IsNullOrEmpty(applicationName)) throw new ArgumentNullException(nameof(applicationName));
            if (string.IsNullOrEmpty(applicationVersion)) throw new ArgumentNullException(nameof(applicationVersion));

            string serverLocation = GetServerLocation();
            string urlQuery = serverLocation + "Toggler/" + toggleName + "/" + toggleValue + "/" + applicationName + "/" + applicationVersion;

            if (_apiCommunication == null) throw new NullReferenceException("API Communication isn't defined");
            TogglerMessage<bool> response = _apiCommunication.ApiGet<TogglerMessage<bool>>(urlQuery).Result;

            return response != null && response.Result;
        }

        /// <inheritdoc />
        protected sealed override string GetServerLocation()
        {
            if (_settings == null) throw new NullReferenceException("Settings not defined");
            if (string.IsNullOrEmpty(_settings.Protocol)) throw new NullReferenceException("Protocol not defined");
            if (string.IsNullOrEmpty(_settings.Server)) throw new NullReferenceException("Server not defined");

            return _settings.Protocol + @"://" + _settings.Server + @"/";// + _settings.Version + @"/";
        }

    }
}
