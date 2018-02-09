using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Farfetch.APIHandler.Authorization.DTO;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Farfetch.Models;
using Farfetch.ServiceFactory;
using Farfetch.UserAccounts.Service;

namespace Farfetch.APIHandler.Authorization
{
    /// <inheritdoc />
    /// <summary>
    /// Handles authorization requests from the Rest API checking against values in the database
    /// </summary>
    public class AuthorizationPublic: IAuthorization, IAuthorizationApi
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

        /// <inheritdoc />
        public TogglerMessage<IEnumerable<UserLoginDto>> GetAll()
        {
            if (_userAccountService == null) throw new NullReferenceException("User Account Service hasn't been defined");
            IEnumerable<User> userList = _userAccountService.GetAll();
            IEnumerable<UserLoginDto> userDtoList = new List<UserLoginDto>();
            if (userList != null && userList.Count() != 0)
            {
                userDtoList = Mapper.Map<IEnumerable<User>, IEnumerable<UserLoginDto>>(userList);
                if (userDtoList == null) throw new AutoMapperMappingException("Error mapping types");
            }
            return new TogglerMessage<IEnumerable<UserLoginDto>>
            {
                Result = userDtoList
            };

        }

        /// <inheritdoc />
        public TogglerMessage<UserLoginDto> Insert(UserLoginDto userDto)
        {
            if (_userAccountService == null) throw new NullReferenceException("User Account Service hasn't been defined");

            User user = Mapper.Map<UserLoginDto, User>(userDto);
            if (user == null) throw new AutoMapperMappingException("Error mapping types");
            _userAccountService.Insert(user);
            user = _userAccountService.GetByExpression(x => x.Username == userDto.Username && x.Password == userDto.Password );
            userDto = Mapper.Map<User, UserLoginDto>(user);
            if (user == null) throw new AutoMapperMappingException("Error mapping types");
            return new TogglerMessage<UserLoginDto>
            {
                Result = userDto,
            };
        }

        /// <inheritdoc />
        public TogglerMessage<UserLoginDto> Update(UserLoginDto userDto)
        {
            if (_userAccountService == null) throw new NullReferenceException("User Account Service hasn't been defined");

            User user = Mapper.Map<UserLoginDto, User>(userDto);
            if (user == null) throw new AutoMapperMappingException("Error mapping types");
            if (user.Id == Guid.Empty)
            {
                user = _userAccountService.GetByExpression(x => x.Username == userDto.Username && x.Password == userDto.Password);
                user.Profile = userDto.Profile;
            }
            _userAccountService.Update(user);
            user = _userAccountService.GetByExpression(x => x.Username == userDto.Username && x.Password == userDto.Password);

            userDto = Mapper.Map<User, UserLoginDto>(user);
            if (user == null) throw new AutoMapperMappingException("Error mapping types");
            return new TogglerMessage<UserLoginDto>
            {
                Result = userDto,
            };
        }

        /// <inheritdoc />
        public TogglerMessage<bool> Delete(string username)
        {
            if (_userAccountService == null) throw new NullReferenceException("User Account Service hasn't been defined");

            User user = _userAccountService.GetByExpression(x => x.Username == username);
            if(user == null) return new TogglerMessage<bool>();
            _userAccountService.Delete(user.Id);
            return new TogglerMessage<bool>();
        }
    }
}