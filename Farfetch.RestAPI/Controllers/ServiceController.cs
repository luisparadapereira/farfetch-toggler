using System;
using System.Collections.Generic;
using Farfetch.APIHandler.API_Toggler.Contract;
using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.DTO;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Farfetch.RestAPI.Controllers
{
    /// <inheritdoc cref="IServiceApi" />
    [Route("[controller]")]
    public class ServiceController: BaseController<IServiceApi>, IServiceApi
    {
        /// <summary>
        /// Initializes the Controller
        /// </summary>
        public ServiceController(IConfiguration config) : base(config)
        {
            GetService(AvailableApis.Service);
        }

        /// <summary>
        /// PRIVATE TO FARFETCH. Gets all the registered services in the database
        /// </summary>
        /// <returns>A FarfetchMessage holding a List of Services</returns>
        [HttpGet]
        [Authorize(Roles = "Admin, Developer")]
        public FarfetchMessage<IEnumerable<ServiceDto>> GetAll()
        {
            return Service?.GetAll();
        }

        /// <summary>
        /// PRIVATE TO FARFETCH. Gets a service from the database
        /// </summary>
        /// <param name="id">The id of the service</param>
        /// <returns>A FarfetchMessage holding the service</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin, Developer")]
        public FarfetchMessage<ServiceDto> Get(Guid id)
        {
            return Service?.Get(id);
        }

        /// <summary>
        /// PRIVATE TO FARFETCH. Inserts a new service
        /// </summary>
        /// <param name="service">The service to insert</param>
        /// <returns></returns>
        /// <remarks>
        /// Mandatory fields:
        /// * Name
        /// * Version
        /// * API Key
        /// </remarks>
        [HttpPost]
        [Authorize(Roles = "Admin, Developer")]
        public FarfetchMessage<ServiceDto> Insert([FromBody] ServiceDto service)
        {
            return Service?.Insert(service);
        }

        /// <summary>
        /// PRIVATE TO FARFETCH. Updates a service
        /// </summary>
        /// <param name="service">The service to update</param>
        /// <returns></returns>
        /// <remarks>
        /// The only updatable field is the API Key
        /// </remarks>
        [HttpPut]
        [Authorize(Roles = "Admin, Developer")]
        public FarfetchMessage<ServiceDto> Update([FromBody] ServiceDto service)
        {
            return Service?.Update(service);
        }

        /// <summary>
        /// PRIVATE TO FARFETCH. Deletes a service.
        /// </summary>
        /// <param name="id">The Id of the service to delete</param>
        /// <returns></returns>
        /// <remarks>
        /// By deleting a service we also delete all the references of that service in toggles
        /// </remarks>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Developer")]
        public FarfetchMessage<bool> Delete(Guid id)
        {
            return Service?.Delete(id);
        }
    }
}
