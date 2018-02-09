using Farfetch.APIHandler.API_Authorization.Contract;
using Farfetch.APIHandler.API_Authorization.Public;
using Farfetch.APIHandler.API_Toggler.Contract;
using Farfetch.APIHandler.API_Toggler.Internal;
using Farfetch.APIHandler.API_Toggler.Public;
using Farfetch.APIHandler.API_UserAccounts.Contract;
using Farfetch.APIHandler.API_UserAccounts.Public;
using Farfetch.APIHandler.Common.Contract;

namespace Farfetch.APIHandler.Common
{
    /// <summary>
    /// The available apis
    /// </summary>
    public enum AvailableApis
    {
        UserAuthorization,
        UserAccounts,
        Toggler,
        TogglerInternal,
        Service
    }

    /// <summary>
    /// Factory that provides apis to clients
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// Given a type of APi returns the instanciated IApi
        /// </summary>
        /// <param name="api">The type of API to query</param>
        /// <returns>An IApi</returns>
        public IApi GetService(AvailableApis api)
        {
            switch (api)
            {
                case AvailableApis.UserAuthorization: return new UserAuthorizationPublic();
                case AvailableApis.UserAccounts: return new UserAccountsPublic();
                case AvailableApis.Toggler: return new TogglerApiPublic();
                case AvailableApis.TogglerInternal: return new TogglerApiInternal();
                case AvailableApis.Service: return new ServiceApiPublic();
                default: return null;
            }
        }
    }
}