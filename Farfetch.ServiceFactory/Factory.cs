using System;
using Farfetch.ServiceManager.Interfaces;
using Farfetch.Toggler.Service;
using Farfetch.UserAccounts.Service;

namespace Farfetch.ServiceFactory
{
    /// <summary>
    /// Available service types
    /// </summary>
    public enum AvailableServices
    {
        Toggler,
        UserAccounts,
        TogglerApplication
    }

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
        public IService GetDbService(AvailableServices service)
        {
            switch (service)
            {
                case AvailableServices.Toggler:
                    return new TogglerService();
                case AvailableServices.UserAccounts:
                    return new UserAccountService();
                case AvailableServices.TogglerApplication:
                    return new ApplicationService();
                default:
                    throw new OperationCanceledException("Service name not found.");
            }
        }
    }
}
