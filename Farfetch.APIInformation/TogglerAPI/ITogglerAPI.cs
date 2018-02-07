using System.Collections.Generic;
using Farfetch.APIHandler.TogglerAPI.DTO;

namespace Farfetch.APIHandler.TogglerAPI
{
    /// <summary>
    /// Defines the contract between clients and the Toggler API
    /// </summary>
    public interface ITogglerApi
    {
        /// <summary>
        /// Gets all the available toggles
        /// </summary>
        /// <returns>An IEnumerable holding all the toggles</returns>
        TogglerMessage<IEnumerable<ToggleDto>> GetAll();

        /// <summary>
        /// Gets a specific toggle for a service
        /// </summary>
        /// <param name="toggleName">The name of the toggle</param>
        /// <param name="toggleValue">The value of the toggle</param>
        /// <param name="serviceName">The name of the service</param>
        /// <param name="serviceVersion">The version of the service</param>
        /// <returns></returns>
        TogglerMessage<bool> GetForService(string toggleName, bool toggleValue, string serviceName, string serviceVersion);

        /// <summary>
        /// Inserts a new toggle
        /// </summary>
        /// <param name="toggleDto">The toggle to insert</param>
        /// <returns>A boolean specifying if the operation was successful or not</returns>
        TogglerMessage<bool> Insert(ToggleDto toggleDto);

        /// <summary>
        /// Updates a toggle
        /// </summary>
        /// <param name="toggleDto">The updated toggle</param>
        /// <returns>A boolean specifying if the operation was successful or not</returns>
        TogglerMessage<bool> Update(ToggleDto toggleDto);

        /// <summary>
        /// Deletes a toggle
        /// </summary>
        /// <param name="toggleDto">The toggle to delete</param>
        /// <returns>A boolean specifying if the operation was successful or not</returns>
        TogglerMessage<bool> Delete(ToggleDto toggleDto);
    }
}