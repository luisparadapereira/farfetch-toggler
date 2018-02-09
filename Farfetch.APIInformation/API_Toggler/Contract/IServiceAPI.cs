using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.TogglerAPI.DTO;

namespace Farfetch.APIHandler.API_Toggler.Contract
{
    /// <summary>
    /// Defines the contract between clients and the Toggler API
    /// </summary>
    public interface IServiceApi: ICrudApi<ServiceDto>
    {
    }
}