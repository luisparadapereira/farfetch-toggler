using System;
using System.Security.Claims;
using Farfetch.APIHandler.Common.Contract;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Farfetch.RestAPI.Controllers
{
    /// <summary>
    /// Service Authentication
    /// </summary>
    [Route("[controller]")]
    public class ServiceAuthenticationController  : BaseAuthenticationController<IApi>, IAuthentication<ServiceDto>
    {
        /// <summary>
        /// Default constructor with DI of config
        /// </summary>
        /// <param name="config">The config file</param>
        public ServiceAuthenticationController(IConfiguration config): base(config)
        {
        }

        /// <inheritdoc />
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateToken([FromBody] ServiceDto serviceLogin)
        {
            if (serviceLogin == null) throw new ArgumentNullException(nameof(serviceLogin));
            if (string.IsNullOrEmpty(serviceLogin.Name)) throw new NullReferenceException("Service Name hasn't been defined");
            if (string.IsNullOrEmpty(serviceLogin.Version)) throw new NullReferenceException("Service Password hasn't been defined");

            IActionResult response = Unauthorized();
            // 1 year
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "Service"),
                new Claim("ServiceName", serviceLogin.Name),
                new Claim("ServiceVersion", serviceLogin.Version)
            };
            string tokenString = BuildToken(DateTime.Now.AddMonths(12), claims);
            response = Ok(new { token = tokenString });

            return response;
        }
    }
}
