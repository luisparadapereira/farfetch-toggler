using System;
using System.Linq;
using Farfetch.Models;
using Farfetch.ServiceManager;
using Farfetch.ServiceManager.Interfaces;
using Ferfetch.Messaging;

namespace Farfetch.Toggler.Service
{
    /// <summary>
    /// Toggler service that handles client requests
    /// </summary>
    public class TogglerService: DbCrudService<Toggle>, IService
    {
        private readonly Broadcaster _broadcaster;


        public TogglerService()
        {
            _broadcaster = new Broadcaster();
        }

        /// <summary>
        /// Overrides the parent class so we can check when inserting
        /// if the item already exists. If it does then updates it.
        /// </summary>
        /// <param name="toggle">The toggle to insert</param>
        public new void Insert(Toggle toggle)
        {
            if (toggle == null) throw new ArgumentNullException(nameof(toggle));
            if(string.IsNullOrEmpty(toggle.Name)) throw new NullReferenceException("Toggle name was not defined");
            Toggle existingToggle = GetBy(x => x.Name == toggle.Name && x.Value == toggle.Value)?.FirstOrDefault();
            if(existingToggle == null)
            {
                base.Insert(toggle);
            }
            else
            {
                toggle.Id = existingToggle.Id;
                Update(toggle);
            }
        }

        /// <summary>
        /// Overrides the parent class so we can emit messages on toggle update
        /// </summary>
        /// <param name="toggle">The toggle to update</param>
        public new void Update(Toggle toggle)
        {
            _broadcaster.SendMessage(toggle.Name, "updated");
            base.Update(toggle);
        }

        /// <summary>
        /// Checks if an operation should be executed or not based on toggle and service values.
        /// Also verifies if there is any other toggle with the same name and service information with the override 
        /// function turned to true. If so that one gains precedence.
        /// </summary>
        /// <param name="toggle">The toggle to check</param>
        /// <returns>boolean specifying if the operation should be executed or not</returns>
        public bool ShouldApplicationExecute(Toggle toggle)
        {
            if (toggle == null) throw new ArgumentNullException(nameof(toggle));
            var service = toggle.ServiceList.FirstOrDefault();
            if (service == null) throw new NullReferenceException("Toggle didn't have any service");

            var results = GetBy(x => x.Name == toggle.Name).ToList();
            if (results.Count == 0) return false;

            var existingToggles = results.Where(y => y.ServiceList.Exists(x => x.Name == service.Name && x.Version == service.Version)).ToList();
            if (existingToggles.Count == 0) return false;
            
            var overrideToggle = results.FirstOrDefault(x => x.Overrides);
            if (overrideToggle != null) return toggle.Value == overrideToggle.Value;

            return results.FirstOrDefault(x => x.Value == toggle.Value) != null;
        }

        /// <summary>
        /// When deleting a service we need to run this to update all the associated toggles
        /// </summary>
        /// <param name="service">The service to delete</param>
        public void RemoveServiceFromToggles(Models.Service service)
        {
            var results = GetBy(x => x.ServiceList.Contains(service));
            foreach (Toggle toggle in results)
            {
                toggle.ServiceList.Remove(service);
                Update(toggle);
            }
        }



    }
}
