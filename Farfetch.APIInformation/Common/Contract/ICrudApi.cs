using System;
using System.Collections.Generic;
using Farfetch.APIHandler.Common.Contract;
using Farfetch.APIHandler.Common.DTO;

namespace Farfetch.APIHandler.Common
{
    public interface ICrudApi<T>: IApi
    {
        /// <summary>
        /// Gets all the entities
        /// </summary>
        /// <returns>An IEnumerable holding all the entities</returns>
        FarfetchMessage<IEnumerable<T>> GetAll();

        /// <summary>
        /// Gets an entity by Id
        /// </summary>
        /// <returns>The entity or null</returns>
        FarfetchMessage<T> Get(Guid id);

        /// <summary>
        /// Inserts a new entity
        /// </summary>
        /// <param name="entity">The entity to insert</param>
        /// <returns>A boolean specifying if the operation was successful or not</returns>
        FarfetchMessage<T> Insert(T entity);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="entity">The updated entity</param>
        /// <returns>A boolean specifying if the operation was successful or not</returns>
        FarfetchMessage<T> Update(T entity);

        /// <summary>
        /// Deletes a entity
        /// </summary>
        /// <param name="id">The id of the toggle to delete</param>
        /// <returns>A boolean specifying if the operation was successful or not</returns>
        FarfetchMessage<bool> Delete(Guid id);
    }
}