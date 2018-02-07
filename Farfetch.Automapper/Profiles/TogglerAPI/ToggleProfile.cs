using AutoMapper;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Farfetch.Models;

namespace Farfetch.Automapper.Profiles.TogglerAPI
{
    /// <summary>
    /// Defines the profiles for type Toggle
    /// </summary>
    internal class ToggleProfile: Profile
    {
        public ToggleProfile()
        {
            CreateMap<Toggle, ToggleDto>()?.ReverseMap();
        }
    }
}