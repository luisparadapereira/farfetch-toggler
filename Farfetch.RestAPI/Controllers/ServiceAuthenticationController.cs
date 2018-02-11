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

        /// <summary>
        /// PRIVATE TO FARFETCH. Creates a new API Key for a service.
        /// </summary>
        /// <param name="serviceDto">The service to generate the API Key</param>
        /// <returns></returns>
        /// <remarks>
        /// API Key for a service is valid for 12 months.
        /// Required fields:
        /// - Name 
        /// - Version
        /// </remarks>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateToken([FromBody] ServiceDto serviceDto)
        {
            if (serviceDto == null) throw new ArgumentNullException(nameof(serviceDto));
            if (string.IsNullOrEmpty(serviceDto.Name)) throw new NullReferenceException("Service Name hasn't been defined");
            if (string.IsNullOrEmpty(serviceDto.Version)) throw new NullReferenceException("Service Password hasn't been defined");

            IActionResult response = Unauthorized();
            // 1 year
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "Service"),
                new Claim("ServiceName", serviceDto.Name),
                new Claim("ServiceVersion", serviceDto.Version)
            };
            string tokenString = BuildToken(DateTime.Now.AddMonths(12), claims);
            response = Ok(new { token = tokenString });

            return response;
        }
    }
}
