using System;
using System.Collections.Generic;
using Farfetch.APIHandler.API_UserAccounts.Contract;
using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Farfetch.RestAPI.Controllers
{
    /// <inheritdoc cref="IUserAccountsApi" />
    [Route("[controller]")]
    public class UserAccountsController : BaseController<IUserAccountsApi>, IUserAccountsApi
    {
        /// <summary>
        /// Initializes the Controller
        /// </summary>
        public UserAccountsController(IConfiguration config) : base(config)
        {
            GetService(AvailableApis.UserAccounts);
        }

        /// <summary>
        /// PRIVATE TO FARFETCH ADMINS. Gets all of the registered users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<IEnumerable<UserLoginDto>> GetAll()
        {
            return Service?.GetAll();
        }

        /// <summary>
        /// PUBLIC. Registers a new public user
        /// </summary>
        /// <param name="user">The user to register</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public FarfetchMessage<UserLoginDto> Insert([FromBody] UserLoginDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.Profile = "Public";
            return Service?.Insert(user);
        }

        /// <summary>
        /// PRIVATE TO FARFETCH ADMINS. Updates the Profile of a user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        /// <remarks>
        /// Only Admins can update a user. 
        /// Available profiles are:
        /// - Admins
        /// - Developer
        /// - Public
        /// </remarks>
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<UserLoginDto> Update([FromBody] UserLoginDto user)
        {
            if (user==null) throw new ArgumentNullException(nameof(user));

            return Service?.Update(user);
        }

        /// <summary>
        /// PRIVATE TO FARFETCH ADMINS. Deletes a user
        /// </summary>
        /// <param name="username">The username of the user to delete</param>
        /// <returns></returns>
        [HttpDelete("{username}")]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<bool> Delete(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));

            return Service?.Delete(username);
        }

        /// <inheritdoc />
        [NonAction]
        public FarfetchMessage<UserLoginDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        [NonAction]
        public FarfetchMessage<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
