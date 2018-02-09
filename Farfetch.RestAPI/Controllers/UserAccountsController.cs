using System;
using System.Collections.Generic;
using Farfetch.APIHandler.API_UserAccounts.Contract;
using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Farfetch.RestAPI.Controllers
{
    /// <inheritdoc cref="IUserAccountsApi" />
    [Route("[controller]")]
    public class UserAccountsController : BaseController<IUserAccountsApi>, IUserAccountsApi
    {
        /// <summary>
        /// Initializes the Controller
        /// </summary>
        public UserAccountsController()
        {
            GetService(AvailableApis.UserAccounts);
        }

        /// <inheritdoc />
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<IEnumerable<UserLoginDto>> GetAll()
        {
            return Service?.GetAll();
        }

        /// <inheritdoc />
        [HttpPost]
        [AllowAnonymous]
        public FarfetchMessage<UserLoginDto> Insert([FromBody] UserLoginDto user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            user.Profile = "Public";
            return Service?.Insert(user);
        }

        /// <inheritdoc />
        [HttpPut]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<UserLoginDto> Update([FromBody] UserLoginDto user)
        {
            if (user==null) throw new ArgumentNullException(nameof(user));

            return Service?.Update(user);
        }

        /// <inheritdoc />
        [HttpDelete("{username}")]
        [Authorize(Roles = "Admin")]
        public FarfetchMessage<bool> Delete(string username)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));

            return Service?.Delete(username);
        }

        /// <inheritdoc />
        public FarfetchMessage<UserLoginDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public FarfetchMessage<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
