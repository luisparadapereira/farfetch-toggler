namespace Farfetch.DB
{
    /// <summary>
    /// Defines the settings to connect to a database
    /// </summary>
    public class DbSettings
    {
        /// <summary>
        /// The Database Type
        /// </summary>
        public string Database { get; set; }

        /// <summary>
        /// The Server Hostname
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// The Port of entry to the Database
        /// </summary>
        public string Port { get; set; }

        /// <summary>
        /// The admin username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// The admin password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// The database we're connecting to
        /// </summary>
        public string DatabaseName { get; set; }
    }
}