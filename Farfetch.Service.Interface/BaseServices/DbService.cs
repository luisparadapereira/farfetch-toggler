using System;
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
        /// <param name="fileSettingsPath">Settings file path</param>
        protected DbService(string fileSettingsPath)
        {
            InitCoreUnit(fileSettingsPath);
        }

        /// <summary>
        /// Initializes the core unit of work
        /// </summary>
        /// <param name="fileSettingsPath">Settings file path</param>
        private void InitCoreUnit(string fileSettingsPath)
        {
            if (fileSettingsPath == null) throw new ArgumentNullException(nameof(fileSettingsPath));
            CoreUnit = new CoreUnit<T>(fileSettingsPath);
        }
    }
}