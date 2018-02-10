using AutoMapper;
using Farfetch.APIHandler.Common.DTO;
using Farfetch.Models;

namespace Farfetch.Automapper.Profiles.Authentication
{
    /// <summary>
    /// Defines the profiles for type Service
    /// </summary>
    public class UserAMProfile: Profile
    {
        /// <inheritdoc />
        public UserAMProfile()
        {
            CreateMap<User, UserLoginDto>()?.ReverseMap();
        }
    }
}