using System;
using Farfetch.ServiceManager.Interfaces;
using Farfetch.Toggler.Service;
using Farfetch.UserAccounts.Service;

namespace Farfetch.ServiceFactory
{
    /// <summary>
    /// Centralized location to retrieve services.
    /// </summary>
    public class Factory
    {
        /// <summary>
        /// Factory returns the service associated to the FactoryService enum
        /// </summary>
        /// <param name="service">The type of service</param>
        /// <returns>An IService</returns>
        public IService GetDbService(FactoryService service)
        {
            switch (service)
            {
                case FactoryService.Toggler:
                    return new TogglerService();
                case FactoryService.UserAccounts:
                    return new UserAccountService();
                case FactoryService.TogglerApplication:
                    return new ApplicationService();
                default:
                    throw new OperationCanceledException("Service name not found.");
            }
        }
    }
}
