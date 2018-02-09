using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.Contract;

namespace Farfetch.APIHandler.API_Toggler.Contract
{
    /// <summary>
    /// Defines the contract between internal clients and the Toggler API
    /// </summary>
    public interface ITogglerApiInternal: IApi
    {
        /// <summary>
        /// Given toggle and service information this class will define what route the application should follow
        /// </summary>
        /// <param name="toggleName">The name of the toggle</param>
        /// <param name="toggleValue">The value of the toggle</param>
        /// <param name="applicationName">The name of the application/service</param>
        /// <param name="applicationVersion">The version of the application/service</param>
        /// <returns>Boolean specifying if the operation should be executed or not</returns>
        bool CheckToggle(string toggleName, bool toggleValue, string applicationName, string applicationVersion, string apiKey);
    }
}