using AutoMapper;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Farfetch.Models;

namespace Farfetch.Automapper.Profiles.TogglerAPI
{
    /// <summary>
    /// Defines the profiles for type Service
    /// </summary>
    public class ServiceAMProfile: Profile
    {
        public ServiceAMProfile()
        {
            CreateMap<Service, ServiceDto>()?.ReverseMap();
        }
    }
}