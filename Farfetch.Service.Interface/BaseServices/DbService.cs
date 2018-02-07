using Farfetch.CoreUnitOfWork;
using Farfetch.Models;

namespace Farfetch.ServiceManager.BaseServices
{
    /// <summary>
    /// Base for services that deal with the database
    /// </summary>
    /// <typeparam name="T">The type of model we're handling</typeparam>
    public class DbService<T> where T : DbT
    {
        /// <summary>
        /// CoreUnitOfWork class. Operations to the database will be exectuted
        /// from here
        /// </summary>
        protected CoreUnit<T> CoreUnit;

        /// <summary>
        /// Default constructor initializes the core unit
        /// </summary>
        protected DbService()
        {
            InitCoreUnit();
        }

        /// <summary>
        /// Initializes the core unit of work
        /// </summary>
        private void InitCoreUnit()
        {
            CoreUnit = new CoreUnit<T>();
        }
    }
}