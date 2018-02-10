namespace Farfetch.Common
{
    public interface IApplication
    {
        /// <summary>
        /// Returns the assembly name
        /// </summary>
        /// <returns>A string holding the assembly name</returns>
        string GetAssemblyName();

        /// <summary>
        /// Returns the assembly version
        /// </summary>
        /// <returns>A string holding the assembly version</returns>
        string GetAssemblyVersion();
    }
}