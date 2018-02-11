using System;
using System.Security.Claims;
using Farfetch.APIHandler.API_Authorization.Contract;
using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Farfetch.RestAPI.Controllers
{
    /// <inheritdoc cref="IUserAuthenticationApi" />
    [Route("[controller]")]
    public class UserAuthenticationController : BaseAuthenticationController<IUserAuthenticationApi>, IUserAuthenticationApi, IAuthentication<UserLoginDto>
    {
        /// <summary>
        /// Default constructor with DI of config
        /// </summary>
        /// <param name="config">The config file</param>
        public UserAuthenticationController(IConfiguration config) : base(config)
        {
            GetService(AvailableApis.UserAuthorization);
        }

        /// <summary>
        /// PUBLIC. Creates a new user token for a valid user
        /// </summary>
        /// <param name="userLogin">The user login</param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public IActionResult CreateToken([FromBody] UserLoginDto userLogin)
        {
            if (userLogin == null) throw new ArgumentNullException(nameof(userLogin));
            if (string.IsNullOrEmpty(userLogin.Username)) throw new NullReferenceException("Username hasn't been defined");
            if (string.IsNullOrEmpty(userLogin.Password)) throw new NullReferenceException("Password hasn't been defined");

            IActionResult response = Unauthorized();

            userLogin = AuthenticateUser(userLogin.Username, userLogin.Password);

            if (userLogin == null) return response;

            var claims = new[]
            {
                new Claim(ClaimTypes.Role, userLogin.Profile)
            };

            string tokenString = BuildToken(DateTime.Now.AddMinutes(30), claims);
            response = Ok(new {token = tokenString});

            return response;
        }

        /// <inheritdoc />
        [NonAction]
        public UserLoginDto AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));
            return Service?.AuthenticateUser(username, password);
        }
    }
}
