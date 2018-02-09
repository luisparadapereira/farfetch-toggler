using System.Collections.Generic;
using Farfetch.APIHandler.Common;
using Farfetch.APIHandler.Common.DTO;

namespace Farfetch.APIHandler.API_UserAccounts.Contract
{
    /// <summary>
    /// Defines the contract between clients and the User Accounts API
    /// </summary>
    public interface IUserAccountsApi: ICrudApi<UserLoginDto>
    {
        /// <summary>
        /// Deletes an user
        /// </summary>
        /// <param name="username">The username of the user to delete</param>
        /// <returns>A boolean specifying if the operation was successful or not</returns>
        FarfetchMessage<bool> Delete(string username);
    }
}