using System;
using AutoMapper;
using Farfetch.APIHandler.API_Authorization.Contract;
using Farfetch.APIHandler.Common.DTO;
using Farfetch.Models;
using Farfetch.ServiceFactory;
using Farfetch.UserAccounts.Service;

namespace Farfetch.APIHandler.API_Authorization.Public
{
    /// <inheritdoc />
    /// <summary>
    /// Handles authorization requests from the Rest API checking against values in the database
    /// </summary>
    public class UserAuthorizationPublic: IUserAuthenticationApi
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly UserAccountService _userAccountService;

        /// <summary>
        /// Default constructor defines the service
        /// </summary>
        public UserAuthorizationPublic(string settingsFilePath)
        {
            Factory factory = new Factory(settingsFilePath);
            _userAccountService = factory.GetDbService(AvailableServices.UserAccounts) as UserAccountService;
        }

        /// <inheritdoc />
        public UserLoginDto AuthenticateUser(string username, string password)
        {
            if (string.IsNullOrEmpty(username)) throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrEmpty(password)) throw new ArgumentNullException(nameof(password));
            if (_userAccountService == null) throw new NullReferenceException("UserAccountService hasn't been initialized yet");

            User user = _userAccountService.GetByExpression(x => x.Username == username && x.Password == password);
            UserLoginDto userLogin = Mapper.Map<User, UserLoginDto>(user);

            return userLogin;
        }
    }
}