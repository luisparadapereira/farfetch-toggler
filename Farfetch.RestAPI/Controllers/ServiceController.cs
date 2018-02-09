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
    /// <inheritdoc cref="IServiceApi" />
    [Route("[controller]")]
    public class ServiceController: BaseController<IServiceApi>, IServiceApi
    {
        /// <summary>
        /// Initializes the Controller
        /// </summary>
        public ServiceController()
        {
            GetService(AvailableApis.Service);
        }

        /// <inheritdoc />
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<IEnumerable<ServiceDto>> GetAll()
        {
            return Service?.GetAll();
        }

        /// <inheritdoc />
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<ServiceDto> Get(Guid id)
        {
            return Service?.Get(id);
        }

        /// <inheritdoc />
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<ServiceDto> Insert([FromBody] ServiceDto service)
        {
            return Service?.Insert(service);
        }

        /// <inheritdoc />
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<ServiceDto> Update([FromBody] ServiceDto service)
        {
            return Service?.Update(service);
        }

        /// <inheritdoc />
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<bool> Delete(Guid id)
        {
            return Service?.Delete(id);
        }
    }
}
