using System;
using System.Linq;
using Farfetch.Toggler.Service;

namespace Farfetch.Toggler.AppManager
{
    public static class CheckToggle
    {
        public static bool ShouldExec(string toggleName, bool toggleValue, string serviceName, string serviceVersion)
        {
            if(string.IsNullOrEmpty(toggleName)) throw new ArgumentNullException(nameof(toggleName));
            if (string.IsNullOrEmpty(serviceName)) throw new ArgumentNullException(nameof(serviceName));
            if (string.IsNullOrEmpty(serviceVersion)) throw new ArgumentNullException(nameof(serviceVersion));

            TogglerService service = new TogglerService();
            if (service == null) throw new NullReferenceException("TogglerService was not defined");
            Models.Service serviceModel = new Models.Service()
            {
                Name = serviceName,
                Version = serviceVersion
            };
            var results = service.GetBy(x => x.Name == toggleName && x.ServiceList.Contains(serviceModel))?.OrderByDescending(x => x?.Overrides).ToList();
            if (results == null || results.Count == 0) return false;
            return results.First()?.Value == toggleValue;
        }
    }
}