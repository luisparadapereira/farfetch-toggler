using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Farfetch.APIHandler.Authorization;
using Farfetch.APIHandler.Authorization.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Farfetch.RestAPI.Controllers
{
    [Route("[controller]")]
    public class AuthenticationController : Controller, IAuthorization
    {
        /// <summary>
        /// The config file holding JWT information
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Default constructor with DI of config
        /// </summary>
        /// <param name="config">The config file</param>
        public AuthenticationController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Creates a new userToken
        /// </summary>
        /// <param name="userLogin">The user information for the token generation</param>
        /// <returns>The token or the error response</returns>
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

            string tokenString = BuildToken(userLogin);
            response = Ok(new {token = tokenString});

            return response;
        }

        /// <inheritdoc />
        public UserLoginDto AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));
            AuthorizationPublic authorization = new AuthorizationPublic();
            return authorization.AuthenticateUser(username, password);
        }

        /// <summary>
        /// Builds the token for the user
        /// </summary>
        /// <param name="user">UserLogin information</param>
        /// <returns>the token string</returns>
        private string BuildToken(UserLoginDto user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Role, user.Profile)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            var clientToken = tokenHandler.WriteToken(token); return clientToken;
        }
    }
}
