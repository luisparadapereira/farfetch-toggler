using AutoMapper;
using Farfetch.APIHandler.TogglerAPI.DTO;
using Farfetch.Models;

namespace Farfetch.Automapper.Profiles.TogglerAPI
{
    /// <summary>
    /// Defines the profiles for type Toggle
    /// </summary>
    public class ToggleAMProfile: Profile
    {
        public ToggleAMProfile()
        {
            CreateMap<Toggle, ToggleDto>()?.ReverseMap();
            CreateMap<Toggle, ToggleListDto>()?.ReverseMap()
                .ForMember(x => x.Value, y => y.Ignore())
                .ForMember(x => x.Overrides, y => y.Ignore())
                .ForMember(x => x.ServiceList, y => y.Ignore());
        }
    }
}