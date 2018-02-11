using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Farfetch.Models;

namespace Farfetch.Repositories
{
    /// <summary>
    /// Defines a repository to deal the database operations
    /// </summary>
    /// <typeparam name="T">Type should be a child of DbT</typeparam>
    public interface IRepository<T> where T: DbT
    {
        /// <summary>
        /// Gets all the entities
        /// </summary>
        /// <returns>A list of entities</returns>
        List<T> GetAll();

        /// <summary>
        /// Gets a list of entities filtered by the expression
        /// </summary>
        /// <param name="expression">The expression to filter the query</param>
        /// <returns>A List of entities</returns>
        List<T> GetAllFiltered(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Gets a single entity filtered by the expression
        /// </summary>
        /// <param name="expression">The expression to filter the query</param>
        /// <returns>The resulting entity or null in case none exists</returns>
        T GetSingle(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Inserts a new entity
        /// </summary>
        /// <param name="value">The entity to insert</param>
        void Insert(T value);

        /// <summary>
        /// Updates an entity
        /// </summary>
        /// <param name="value">The entity to update</param>
        void Update(T value);

        /// <summary>
        /// Updates an entity based on an expression
        /// </summary>
        /// <param name="expression">The expression to query for a single entity to update</param>
        /// <param name="value">The entity to update</param>
        void Update(Expression<Func<T, bool>> expression, T value);

        /// <summary>
        /// Deletes a single entity that matches the expression
        /// </summary>
        /// <param name="expression">The expression to search the entity to delete by</param>
        void Delete(Expression<Func<T, bool>> expression);

    }
}