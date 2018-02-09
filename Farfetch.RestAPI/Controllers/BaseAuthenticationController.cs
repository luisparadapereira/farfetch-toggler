using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Farfetch.RestAPI.Controllers
{
    /// <summary>
    /// The base for the authorization controllers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseAuthenticationController<T>: BaseController<T> where T: IApi
    {
        /// <summary>
        /// The config file holding JWT information
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Constructor with DI of configuration
        /// </summary>
        /// <param name="config">The configuration</param>
        public BaseAuthenticationController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Generates the token
        /// </summary>
        /// <param name="timeUntilExpiration"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public string BuildToken(DateTime timeUntilExpiration, Claim[] claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: timeUntilExpiration,
                signingCredentials: creds);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var clientToken = tokenHandler.WriteToken(token); return clientToken;
        }
    }
}