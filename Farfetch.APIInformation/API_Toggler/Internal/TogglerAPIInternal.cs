using System;
using Farfetch.APIHandler.API_Toggler.Contract;
using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.DTO;
using Farfetch.Common;

namespace Farfetch.APIHandler.API_Toggler.Internal
{
    /// <inheritdoc />
    /// <summary>
    /// internally used class to handle all requests made by applications to the Toggler API
    /// </summary>
    public class TogglerApiInternal : ExternalApi, ITogglerApiInternal
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
        private const string SETTINGS_FILEPATH = @"API_Toggler/restSettings.json";

        /// <summary>
        /// Default constructor initializes communications and the settings
        /// </summary>
        public TogglerApiInternal()
        {
            _apiCommunication = new ApiCommunication();
            SettingsReader<ApiSettings> reader = new SettingsReader<ApiSettings>();
            _settings = reader.Read(SETTINGS_FILEPATH);
        }


        /// <inheritdoc />
        public bool CheckToggle(string toggleName, bool toggleValue, string applicationName, string applicationVersion, string apiKey)
        {
            if (string.IsNullOrEmpty(toggleName)) throw new ArgumentNullException(nameof(toggleName));
            if (string.IsNullOrEmpty(applicationName)) throw new ArgumentNullException(nameof(applicationName));
            if (string.IsNullOrEmpty(applicationVersion)) throw new ArgumentNullException(nameof(applicationVersion));

            string serverLocation = GetServerLocation();
            string urlQuery = serverLocation + "Toggler/" + toggleName + "/" + toggleValue + "/" + applicationName + "/" + applicationVersion;

            if (_apiCommunication == null) throw new NullReferenceException("API Communication isn't defined");
            FarfetchMessage<bool> response = _apiCommunication.ApiGet<FarfetchMessage<bool>>(urlQuery, apiKey).Result;

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
