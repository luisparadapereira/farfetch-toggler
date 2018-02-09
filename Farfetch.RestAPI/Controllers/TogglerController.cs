using System;
using System.Collections.Generic;
using Farfetch.APIHandler.API_Toggler.Contract;
using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.DTO;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farfetch.RestAPI.Controllers
{
    /// <inheritdoc cref="ITogglerApi" />
    [Route("[controller]")]
    public class TogglerController : BaseController<ITogglerApi>, ITogglerApi
    {
        /// <summary>
        /// Initializes the Controller
        /// </summary>
        public TogglerController()
        {
            GetService(AvailableApis.Toggler);
        }

        /// <inheritdoc />
        [HttpGet]
        [Authorize]
        public FarfetchMessage<IEnumerable<ToggleListDto>> GetAll()
        {
            return Service?.GetAll();
        }

        /// <inheritdoc />
        [HttpGet("{id}")]
        [Authorize]
        public FarfetchMessage<ToggleDto> Get(Guid id)
        {
           return Service?.Get(id);
        }

        /// <inheritdoc/>
        [HttpGet("{toggleName}/{toggleValue}/{serviceName}/{serviceVersion}")]
        [Authorize]
        public FarfetchMessage<bool> GetForService(string toggleName, bool toggleValue, string serviceName, string serviceVersion)
        {
            if (string.IsNullOrEmpty(toggleName)) throw new ArgumentNullException(nameof(toggleName));
            if (string.IsNullOrEmpty(serviceName)) throw new ArgumentNullException(nameof(serviceName));
            if (string.IsNullOrEmpty(serviceVersion)) throw new ArgumentNullException(nameof(serviceVersion));

            return Service?.GetForService(toggleName, toggleValue, serviceName, serviceVersion);
        }

        /// <inheritdoc />
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<ToggleDto> Insert([FromBody] ToggleDto toggleDto)
        {
            return Service?.Insert(toggleDto);
        }

        /// <inheritdoc />
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<ToggleDto> Update([FromBody] ToggleDto toggleDto)
        {
            return Service?.Update(toggleDto);
        }

        /// <inheritdoc />
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<bool> Delete(Guid id)
        {
            return Service?.Delete(id);
        }

        /// <inheritdoc />
        FarfetchMessage<IEnumerable<ToggleDto>> ICrudApi<ToggleDto>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
