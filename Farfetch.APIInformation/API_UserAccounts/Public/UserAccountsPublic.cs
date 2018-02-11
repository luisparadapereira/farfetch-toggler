using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Farfetch.APIHandler.API_UserAccounts.Contract;
using Farfetch.APIHandler.Common.DTO;
using Farfetch.Models;
using Farfetch.ServiceFactory;
using Farfetch.UserAccounts.Service;

namespace Farfetch.APIHandler.API_UserAccounts.Public
{
    public class UserAccountsPublic : IUserAccountsApi
    {
        /// <summary>
        /// The user service
        /// </summary>
        private readonly UserAccountService _userAccountService;

        /// <summary>
        /// Default constructor defines the service
        /// </summary>
        public UserAccountsPublic(string settingsFilePath)
        {
            Factory factory = new Factory(settingsFilePath);
            _userAccountService = factory.GetDbService(AvailableServices.UserAccounts) as UserAccountService;
        }

        /// <inheritdoc />
        public FarfetchMessage<IEnumerable<UserLoginDto>> GetAll()
        {
            if (_userAccountService == null) throw new NullReferenceException("User Account Service hasn't been defined");
            IEnumerable<User> userList = _userAccountService.GetAll();
            IEnumerable<UserLoginDto> userDtoList = new List<UserLoginDto>();
            if (userList != null && userList.Count() != 0)
            {
                userDtoList = Mapper.Map<IEnumerable<User>, IEnumerable<UserLoginDto>>(userList);
                if (userDtoList == null) throw new AutoMapperMappingException("Error mapping types");
            }
            return new FarfetchMessage<IEnumerable<UserLoginDto>>
            {
                Result = userDtoList
            };

        }

        /// <inheritdoc />
        public FarfetchMessage<UserLoginDto> Get(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public FarfetchMessage<UserLoginDto> Insert(UserLoginDto userDto)
        {
            if (_userAccountService == null) throw new NullReferenceException("User Account Service hasn't been defined");

            User user = Mapper.Map<UserLoginDto, User>(userDto);
            if (user == null) throw new AutoMapperMappingException("Error mapping types");
            _userAccountService.Insert(user);
            user = _userAccountService.GetByExpression(x => x.Username == userDto.Username && x.Password == userDto.Password);
            userDto = Mapper.Map<User, UserLoginDto>(user);
            if (user == null) throw new AutoMapperMappingException("Error mapping types");
            return new FarfetchMessage<UserLoginDto>
            {
                Result = userDto,
            };
        }

        /// <inheritdoc />
        public FarfetchMessage<UserLoginDto> Update(UserLoginDto userDto)
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
            return new FarfetchMessage<UserLoginDto>
            {
                Result = userDto,
            };
        }

        /// <inheritdoc />
        public FarfetchMessage<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public FarfetchMessage<bool> Delete(string username)
        {
            if (_userAccountService == null) throw new NullReferenceException("User Account Service hasn't been defined");
            User user = _userAccountService.GetByExpression(x => x.Username == username);
            if (user == null)
            {
                return new FarfetchMessage<bool>
                {
                    Result = false
                };
            }
            try
            {
                _userAccountService.Delete(user.Id);
                return new FarfetchMessage<bool>
                {
                    Result = true
                };
            }
            catch (Exception)
            {
                return new FarfetchMessage<bool>
                {
                    Result = false
                };
            }
        }
    }
}