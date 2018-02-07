using Farfetch.APIHandler.Authorization.DTO;

namespace Farfetch.APIHandler.Authorization
{
    /// <summary>
    /// Defines the contract between the clients and the Authorization methods
    /// </summary>
    public interface IAuthorization
    {
        /// <summary>
        /// Authorizes a user comparing username and password against the database
        /// </summary>
        /// <param name="username">the username to compare</param>
        /// <param name="password">the password to compare</param>
        /// <returns>The UserLoginDto if successful. Null if not</returns>
        UserLoginDto AuthenticateUser(string username, string password);
    }
}