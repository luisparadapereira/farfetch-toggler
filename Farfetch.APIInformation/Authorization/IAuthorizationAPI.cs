using System.Collections.Generic;
using Farfetch.APIHandler.Authorization.DTO;
using Farfetch.APIHandler.TogglerAPI.DTO;

namespace Farfetch.APIHandler.Authorization
{
    public interface IAuthorizationApi
    {
        /// <summary>
        /// Gets all the available users
        /// </summary>
        /// <returns>An IEnumerable holding all the users</returns>
        TogglerMessage<IEnumerable<UserLoginDto>> GetAll();

        /// <summary>
        /// Inserts a new user
        /// </summary>
        /// <param name="userDto">The user to insert</param>
        /// <returns>A boolean specifying if the operation was successful or not</returns>
        TogglerMessage<UserLoginDto> Insert(UserLoginDto userDto);

        /// <summary>
        /// Updates an user
        /// </summary>
        /// <param name="userDto">The updated user</param>
        /// <returns>A boolean specifying if the operation was successful or not</returns>
        TogglerMessage<UserLoginDto> Update(UserLoginDto userDto);

        /// <summary>
        /// Deletes an user
        /// </summary>
        /// <param name="username">The username of the user to delete</param>
        /// <returns>A boolean specifying if the operation was successful or not</returns>
        TogglerMessage<bool> Delete(string username);
    }
}