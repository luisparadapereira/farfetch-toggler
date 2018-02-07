using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Farfetch.ServiceManager.Interfaces
{
    public interface ICrudService<T>
    {
        /// <summary>
        /// Gets all the available structures of type T
        /// </summary>
        /// <returns>An IEnumerable. Null if none</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Gets all the structures that match the predicate
        /// </summary>
        /// <param name="expression">The predicate to test</param>
        /// <returns>An IEnumerable. Null if none</returns>
        IEnumerable<T> GetBy(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Gets the item of type T that has the same Id has the input
        /// </summary>
        /// <param name="id">A Guid holding the Id to match</param>
        /// <returns>The matching item retrived from the database or null in case it doesn't find it</returns>
        T GetById(Guid id);

        /// <summary>
        /// Gets the item of type T that fulfills the expression
        /// </summary>
        /// <param name="expression">The predicate to test</param>
        /// <returns>The matching item retrived from the database or null in case it doesn't find it</returns>
        T GetByExpression(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Inserts a new value into the database
        /// </summary>
        /// <param name="value">The value to insert</param>
        void Insert(T value);

        /// <summary>
        /// Updates an entry in the database
        /// </summary>
        /// <param name="value">The value update</param>
        void Update(T value);

        /// <summary>
        /// Deletes an entry from the database with matching Id
        /// </summary>
        /// <param name="id">The Id to search for</param>
        void Delete(Guid id);

        /// <summary>
        /// Deletes a list of entries from the database with matching Id
        /// </summary>
        /// <param name="idsToDelete">A list of ids to delete</param>
        void Delete(IEnumerable<Guid> idsToDelete);
    }
}
