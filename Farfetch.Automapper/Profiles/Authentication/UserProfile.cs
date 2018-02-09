using AutoMapper;
using Farfetch.APIHandler.Common.DTO;
using Farfetch.Models;

namespace Farfetch.Automapper.Profiles.Authentication
{
    /// <summary>
    /// Defines the profiles for type Service
    /// </summary>
    internal class UserProfile: Profile
    {
        /// <inheritdoc />
        public UserProfile()
        {
            CreateMap<User, UserLoginDto>()?.ReverseMap();
        }
    }
}