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
    [Route("[controller]")]
    public class TogglerController : Controller, ITogglerApi
    {
        /// <inheritdoc />
        [HttpGet]
        [Authorize]
        public TogglerMessage<IEnumerable<ToggleDto>> GetAll()
        {
            //var currentUser = HttpContext.User;

            //string userType = currentUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value;
            TogglerApiPublic togglerPublic = new TogglerApiPublic();
            return togglerPublic.GetAll();

        }

        /// <inheritdoc/>
        [HttpGet("{toggleName}/{toggleValue}/{serviceName}/{serviceVersion}")]
        [Authorize]
        public TogglerMessage<bool> GetForService(string toggleName, bool toggleValue, string serviceName, string serviceVersion)
        {
            if (string.IsNullOrEmpty(toggleName)) throw new ArgumentNullException(nameof(toggleName));
            if (string.IsNullOrEmpty(serviceName)) throw new ArgumentNullException(nameof(serviceName));
            if (string.IsNullOrEmpty(serviceVersion)) throw new ArgumentNullException(nameof(serviceVersion));

            TogglerApiPublic togglerPublic = new TogglerApiPublic();
            return togglerPublic.GetForService(toggleName, toggleValue, serviceName, serviceVersion);
        }

        /// <inheritdoc />
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public TogglerMessage<bool> Insert([FromBody] ToggleDto toggleDto)
        {
            TogglerApiPublic togglerPublic = new TogglerApiPublic();
            return togglerPublic.Insert(toggleDto);
        }

        /// <inheritdoc />
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public TogglerMessage<bool> Update([FromBody] ToggleDto toggleDto)
        {
            TogglerApiPublic togglerPublic = new TogglerApiPublic();
            return togglerPublic.Update(toggleDto);
        }

        /// <inheritdoc />
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public TogglerMessage<bool> Delete([FromBody] ToggleDto toggleDto)
        {
            TogglerApiPublic togglerPublic = new TogglerApiPublic();
            return togglerPublic.Delete(toggleDto);
        }
    }
}
