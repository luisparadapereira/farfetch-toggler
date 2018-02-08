using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Farfetch.APIHandler.TogglerAPI;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farfetch.RestAPI.Controllers
{
    /// <inheritdoc cref="ITogglerApi" />
    [Route("[controller]")]
    public class ServiceController: Controller
    {
        /// <inheritdoc />
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public TogglerMessage<IEnumerable<ServiceDto>> GetAllServices()
        {
            TogglerApiPublic togglerPublic = new TogglerApiPublic();
            return togglerPublic.GetAllServices();
        }

        /// <inheritdoc />
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public TogglerMessage<ServiceDto> GetService(Guid id)
        {
            TogglerApiPublic togglerPublic = new TogglerApiPublic();
            return togglerPublic.GetService(id);
        }

        /// <inheritdoc />
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public TogglerMessage<ServiceDto> InsertService([FromBody] ServiceDto service)
        {
            TogglerApiPublic togglerPublic = new TogglerApiPublic();
            return togglerPublic.InsertService(service);
        }

        /// <inheritdoc />
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public TogglerMessage<ServiceDto> UpdateService([FromBody] ServiceDto service)
        {
            TogglerApiPublic togglerPublic = new TogglerApiPublic();
            return togglerPublic.UpdateService(service);
        }

        /// <inheritdoc />
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public TogglerMessage<bool> DeleteService(Guid id)
        {
            TogglerApiPublic togglerPublic = new TogglerApiPublic();
            return togglerPublic.DeleteService(id);
        }



    }
}
