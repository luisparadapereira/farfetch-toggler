using System.Collections.Generic;
using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.DTO;
using Farfetch.APIHandler.TogglerAPI.DTO;

namespace Farfetch.APIHandler.API_Toggler.Contract
{
    /// <summary>
    /// Defines the contract between clients and the Toggler API
    /// </summary>
    public interface ITogglerApi: ICrudApi<ToggleDto>
    {
        /// <summary>
        /// Gets all the entities
        /// </summary>
        /// <returns>An IEnumerable holding all the entities</returns>
        new FarfetchMessage<IEnumerable<ToggleListDto>> GetAll();

        /// <summary>
        /// Gets a specific toggle for a service
        /// </summary>
        /// <param name="toggleName">The name of the toggle</param>
        /// <param name="toggleValue">The value of the toggle</param>
        /// <param name="serviceName">The name of the service</param>
        /// <param name="serviceVersion">The version of the service</param>
        /// <returns></returns>
        FarfetchMessage<bool> GetForService(string toggleName, bool toggleValue, string serviceName, string serviceVersion);
    }
}