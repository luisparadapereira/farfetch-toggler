using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Farfetch.RestAPI.Controllers
{

    [Route("[controller]")]
    public class ServiceAuthorizationController : Controller
    {
        /// <summary>
        /// The config file holding JWT information
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Default constructor with DI of config
        /// </summary>
        /// <param name="config">The config file</param>
        public ServiceAuthorizationController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Creates a new userToken
        /// </summary>
        /// <param name="serviceLogin">The service information for the token generation</param>
        /// <returns>The token or the error response</returns>
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateToken([FromBody] ServiceDto serviceLogin)
        {
            if (serviceLogin == null) throw new ArgumentNullException(nameof(serviceLogin));
            if (string.IsNullOrEmpty(serviceLogin.Name)) throw new NullReferenceException("Service Name hasn't been defined");
            if (string.IsNullOrEmpty(serviceLogin.Version)) throw new NullReferenceException("Service Password hasn't been defined");

            IActionResult response = Unauthorized();

            string tokenString = BuildToken(serviceLogin);
            response = Ok(new { token = tokenString });

            return response;
        }

        /// <summary>
        /// Builds the token for the service
        /// </summary>
        /// <param name="service">UserLogin information</param>
        /// <returns>the token string</returns>
        private string BuildToken(ServiceDto service)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, "Service"),
                new Claim("serviceName", service.Name),
                new Claim("serviceVersion", service.Version),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMonths(12),
                signingCredentials: creds);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var clientToken = tokenHandler.WriteToken(token); return clientToken;
        }
    }
}
