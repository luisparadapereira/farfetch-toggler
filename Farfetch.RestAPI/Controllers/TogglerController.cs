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
    /// <inheritdoc cref="ITogglerApi" />
    [Route("[controller]")]
    public class TogglerController : BaseController<ITogglerApi>, ITogglerApi
    {
        /// <summary>
        /// Initializes the Controller
        /// </summary>
        public TogglerController(IConfiguration config) : base(config)
        {
            GetService(AvailableApis.Toggler);
        }

        /// <summary>
        /// PUBLIC. Gets all the toggles available
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        public FarfetchMessage<IEnumerable<ToggleListDto>> GetAll()
        {
            return Service?.GetAll();
        }

        /// <summary>
        /// PUBLIC. Gets a single toggle information
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [Authorize]
        public FarfetchMessage<ToggleDto> Get(Guid id)
        {
           return Service?.Get(id);
        }

        /// <summary>
        /// AUTHORIZED SERVICES ONLY. Checks if a toggle should be or not executed for a given service
        /// </summary>
        /// <param name="toggleName">The name of the toggle</param>
        /// <param name="toggleValue">The value of the toggle</param>
        /// <param name="serviceName">The name of the service</param>
        /// <param name="serviceVersion">The version fo the service</param>
        /// <returns></returns>
        [HttpGet("{toggleName}/{toggleValue}/{serviceName}/{serviceVersion}")]
        [Authorize]
        public FarfetchMessage<bool> GetForService(string toggleName, bool toggleValue, string serviceName, string serviceVersion)
        {
            if (string.IsNullOrEmpty(toggleName)) throw new ArgumentNullException(nameof(toggleName));
            if (string.IsNullOrEmpty(serviceName)) throw new ArgumentNullException(nameof(serviceName));
            if (string.IsNullOrEmpty(serviceVersion)) throw new ArgumentNullException(nameof(serviceVersion));

            return Service?.GetForService(toggleName, toggleValue, serviceName, serviceVersion);
        }

        /// <summary>
        /// PRIVATE TO FARFETCH. Inserts a new toggle
        /// </summary>
        /// <param name="toggleDto"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(Roles = "Admin, Developer")]
        public FarfetchMessage<ToggleDto> Insert([FromBody] ToggleDto toggleDto)
        {
            return Service?.Insert(toggleDto);
        }

        /// <summary>
        /// PRIVATE TO FARFETCH. Updates a toggle
        /// </summary>
        /// <param name="toggleDto"></param>
        /// <returns></returns>
        /// <remarks>
        /// A toggle can only have the following fields updated:
        /// - Value
        /// - Override
        /// - ServiceList
        /// </remarks>
        [HttpPut]
        [Authorize(Roles = "Admin, Developer")]
        public FarfetchMessage<ToggleDto> Update([FromBody] ToggleDto toggleDto)
        {
            return Service?.Update(toggleDto);
        }

        /// <summary>
        /// PRIVATE TO FARFETCH. Deletes a toggle
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Developer")]
        public FarfetchMessage<bool> Delete(Guid id)
        {
            return Service?.Delete(id);
        }

        /// <inheritdoc />
        [NonAction]
        FarfetchMessage<IEnumerable<ToggleDto>> ICrudApi<ToggleDto>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
