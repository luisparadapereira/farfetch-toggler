using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.Contract;
using Microsoft.AspNetCore.Mvc;

namespace Farfetch.RestAPI.Controllers
{
    /// <summary>
    /// The base controller
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseController<T>: Controller where T: IApi
    {
        /// <summary>
        /// Gets a service
        /// </summary>
        /// <param name="api">The choice of API</param>
        protected void GetService(AvailableApis api)
        {
            Factory factory = new Factory();
            Service = (T) factory.GetService(api);
        }

        /// <summary>
        /// The service
        /// </summary>
        protected T Service { get; private set; }
    }
}