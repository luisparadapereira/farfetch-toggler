namespace Farfetch.APIHandler.Common
{
    /// <summary>
    /// Defines the methods for an external api
    /// </summary>
    public abstract class ExternalApi
    {
        /// <summary>
        /// Returns the server location for client connections
        /// </summary>
        /// <returns>A string holding the location</returns>
        protected abstract string GetServerLocation();

    }
}