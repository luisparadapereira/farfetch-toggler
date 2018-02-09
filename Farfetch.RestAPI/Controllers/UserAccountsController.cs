using System;
using System.Collections.Generic;
using Farfetch.APIHandler.Authorization;
using Farfetch.APIHandler.Authorization.DTO;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farfetch.RestAPI.Controllers
{
    /// <inheritdoc cref="IAuthorizationApi" />
    [Route("[controller]")]
    public class UserAccountsController : Controller, IAuthorizationApi
    {
        /// <inheritdoc />
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public TogglerMessage<IEnumerable<UserLoginDto>> GetAll()
        {
            AuthorizationPublic authPublic = new AuthorizationPublic();
            return authPublic.GetAll();
        }

        /// <inheritdoc />
        [HttpPost]
        [AllowAnonymous]
        public TogglerMessage<UserLoginDto> Insert([FromBody] UserLoginDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.Profile = "Public";
            AuthorizationPublic authPublic = new AuthorizationPublic();
            return authPublic.Insert(user);
        }

        /// <inheritdoc />
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public TogglerMessage<UserLoginDto> Update([FromBody] UserLoginDto user)
        {
            if (user==null) throw new ArgumentNullException(nameof(user));

            AuthorizationPublic authPublic = new AuthorizationPublic();
            return authPublic.Update(user);
        }

        /// <inheritdoc />
        [HttpDelete("{username}")]
        [Authorize(Roles = "Admin")]
        public TogglerMessage<bool> Delete(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));

            AuthorizationPublic authPublic = new AuthorizationPublic();
            return authPublic.Delete(username);
        }
    }
}
