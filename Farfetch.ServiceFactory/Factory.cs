using System;
using Farfetch.ServiceManager.Interfaces;
using Farfetch.Toggler.Service;
using Farfetch.UserAccounts.Service;

namespace Farfetch.ServiceFactory
{
    public class Factory
    {
        public IService GetService(string serviceName)
        {
            switch (serviceName)
            {
                case "togglerService":
                    return new TogglerService();
                case "userAccountService":
                    return new UserAccountService();
                default:
                    throw new OperationCanceledException("Service name not found. <<" + serviceName + ">>");
            }
        }
    }
}
