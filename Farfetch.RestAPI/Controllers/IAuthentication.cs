using Microsoft.AspNetCore.Mvc;

namespace Farfetch.RestAPI.Controllers
{
    /// <summary>
    /// Contract to define 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IAuthentication<T>
    {
        /// <summary>
        /// Creates a token for the entity
        /// </summary>
        /// <param name="entity">The entity to create a token</param>
        /// <returns>The Http response with the token</returns>
        IActionResult CreateToken([FromBody] T entity);
    }
}