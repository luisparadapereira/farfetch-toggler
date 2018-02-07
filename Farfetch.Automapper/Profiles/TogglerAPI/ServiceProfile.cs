using AutoMapper;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Farfetch.Models;

namespace Farfetch.Automapper.Profiles.TogglerAPI
{
    /// <summary>
    /// Defines the profiles for type Service
    /// </summary>
    internal class ServiceProfile: Profile
    {
        public ServiceProfile()
        {
            CreateMap<Service, ServiceDto>()?.ReverseMap();
        }
    }
}