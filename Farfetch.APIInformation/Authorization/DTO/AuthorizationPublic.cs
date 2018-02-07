using System;
using AutoMapper;
using Farfetch.Models;
using Farfetch.ServiceFactory;
using Farfetch.UserAccounts.Service;

namespace Farfetch.APIHandler.Authorization.DTO
{
    /// <inheritdoc />
    /// <summary>
    /// Handles authorization requests from the Rest API checking against values in the database
    /// </summary>
    public class AuthorizationPublic: IAuthorization
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly UserAccountService _userAccountService;

        /// <summary>
        /// Default constructor defines the service
        /// </summary>
        public AuthorizationPublic()
        {
            Factory factory = new Factory();
            _userAccountService = factory.GetDbService(FactoryService.UserAccounts) as UserAccountService;
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