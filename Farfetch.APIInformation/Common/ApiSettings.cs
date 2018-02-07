namespace Farfetch.APIHandler.Common
{
    /// <summary>
    /// Settings for an API Connection
    /// </summary>
    public class ApiSettings
    {
        /// <summary>
        /// The protocol to use when connecting to the API
        /// ex.: HTTP, HTTPS
        /// </summary>
        public string Protocol { get; set; }

        /// <summary>
        ///  The server address
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The version of the API
        /// </summary>
        public string Version { get; set; }
    }
}