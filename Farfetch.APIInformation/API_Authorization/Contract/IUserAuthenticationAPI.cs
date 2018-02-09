using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.Contract;
using Farfetch.APIHandler.Common.DTO;

namespace Farfetch.APIHandler.API_Authorization.Contract
{
    public interface IUserAuthenticationApi: IApi
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