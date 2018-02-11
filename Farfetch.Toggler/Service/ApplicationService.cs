using System;
using System.Linq;
using Farfetch.ServiceManager;
using Farfetch.ServiceManager.Interfaces;

namespace Farfetch.Toggler.Service
{
    /// <summary>
    /// Models.Servicer service that handles client requests
    /// </summary>
    public class ApplicationService: DbCrudService<Models.Service>, IService
    {
        private string _fileSettingsPath;

        public ApplicationService(string fileSettingsPath) : base(fileSettingsPath)
        {
            _fileSettingsPath = fileSettingsPath;
        }

        /// <summary>
        /// Overrides the parent class so we can check when inserting
        /// if the item already exists. If it does then updates it.
        /// </summary>
        /// <param name="service">The service to insert</param>
        public new void Insert(Models.Service service)
        {
            if (service == null) throw new ArgumentNullException(nameof(service));
            if(string.IsNullOrEmpty(service.Name)) throw new NullReferenceException("service name was not defined");
            if (string.IsNullOrEmpty(service.Version)) throw new NullReferenceException("service version was not defined");
            Models.Service existingService = GetBy(x => x.Name == service.Name && x.Version == service.Version)?.FirstOrDefault();
            if(existingService == null)
            {
                base.Insert(service);
            }
            else
            {
                service.Id = existingService.Id;
                Update(service);
            }
        }

        public new void Delete(Guid id)
        {
            Models.Service service = GetById(id);
            if (service == null) return;
            TogglerService togglerService = new TogglerService(_fileSettingsPath);
            togglerService.RemoveServiceFromToggles(service);
            base.Delete(id);
        }
    }
}
