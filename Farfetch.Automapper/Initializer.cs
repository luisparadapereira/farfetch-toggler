using AutoMapper;
using Farfetch.Automapper.Profiles.Authentication;
using Farfetch.Automapper.Profiles.TogglerAPI;

namespace Farfetch.Automapper
{
    /// <summary>
    /// Initialization of the AutoMapper
    /// </summary>
    public class Initializer
    {
        public void RegisterMappings()
        {
            Mapper.Reset();

            Mapper.Initialize(
                cfg =>
                {
                   cfg?.AddProfile<ToggleProfile>();
                   cfg?.AddProfile<ServiceProfile>();
                    cfg?.AddProfile<UserProfile>();
                }
            );
        }
    }
}
