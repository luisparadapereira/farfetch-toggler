using System;
using System.Diagnostics;
using System.Reflection;

namespace Farfetch.AppCommon
{
    /// <summary>
    /// Defines application information such as assembly name and version
    /// </summary>
    public class ServiceInformation
    {
        /// <summary>
        /// The assembly that has called this class
        /// </summary>
        private Assembly _callingAssembly;

        /// <summary>
        /// The version of the assembly as defined in properties
        /// </summary>
        protected string CallingAssemblyVersion;

        /// <summary>
        /// The name of the assembly
        /// </summary>
        protected string CallingAssemblyName;

        /// <summary>
        /// Gathers information on the input assembly type
        /// </summary>
        /// <param name="assemblyType">The type of the assembly to get.</param>
        protected void GetApplicationInformation(Type assemblyType)
        {
            _callingAssembly = Assembly.GetAssembly(assemblyType);
            if(_callingAssembly == null) throw new NullReferenceException("Couldn't get the Assembly information");
            CallingAssemblyName = _callingAssembly?.GetName()?.Name;
            if (_callingAssembly.Location == null) throw new NullReferenceException("Couldn't get the Assembly Location information");
            CallingAssemblyVersion = FileVersionInfo.GetVersionInfo(_callingAssembly.Location).FileVersion;
        }
    }
}
