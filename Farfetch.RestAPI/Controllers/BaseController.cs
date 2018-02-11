using System;
using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Farfetch.RestAPI.Controllers
{
    /// <summary>
    /// The base controller
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseController<T>: Controller where T: IApi
    {

        /// <summary>
        /// The config file holding JWT information
        /// </summary>
        protected readonly IConfiguration _config;

        public BaseController(IConfiguration config)
        {
            _config = config;
        }

        /// <summary>
        /// Gets a service
        /// </summary>
        /// <param name="api">The choice of API</param>
        protected void GetService(AvailableApis api)
        {
            string settingsFilePath = _config.GetSection("DatabaseSettings").Value;
            if (string.IsNullOrEmpty(settingsFilePath)) throw new NullReferenceException("Couldn't read settings file");
            Factory factory = new Factory(settingsFilePath);
            Service = (T) factory.GetService(api);
        }

        /// <summary>
        /// The service
        /// </summary>
        protected T Service { get; private set; }
    }
}