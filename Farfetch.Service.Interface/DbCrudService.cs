using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Farfetch.Models;
using Farfetch.ServiceManager.BaseServices;
using Farfetch.ServiceManager.Interfaces;

namespace Farfetch.ServiceManager
{
    public class DbCrudService<T>: DbService<T>, ICrudService<T> where T: DbT
    {
        /// <inheritdoc />
        public IEnumerable<T> GetAll()
        {
            if (CoreUnit == null) throw new NullReferenceException("CoreUnit wasn't initialized");
            if (CoreUnit.Repository == null) throw new NullReferenceException("Repository wasn't initialized");

            var allResults = CoreUnit.Repository.GetAll();
            return allResults?.Count > 0 ? allResults : null;
        }

        /// <inheritdoc />
        public IEnumerable<T> GetBy(Expression<Func<T, bool>> expression)
        {
            if (CoreUnit == null) throw new NullReferenceException("CoreUnit wasn't initialized");
            if (CoreUnit.Repository == null) throw new NullReferenceException("Repository wasn't initialized");

            var allResults = CoreUnit.Repository.GetAllFiltered(expression);
            return allResults?.Count > 0 ? allResults : null;
        }

        /// <inheritdoc />
        public T GetById(Guid id)
        {
            if (CoreUnit == null) throw new NullReferenceException("CoreUnit wasn't initialized");
            if (CoreUnit.Repository == null) throw new NullReferenceException("Repository wasn't initialized");

            var result = CoreUnit.Repository.GetSingle(x => x.Id == id);
            return result;
        }

        /// <inheritdoc />
        public void Insert(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (CoreUnit == null) throw new NullReferenceException("CoreUnit wasn't initialized");
            if (CoreUnit.Repository == null) throw new NullReferenceException("Repository wasn't initialized");

            value.Id = Guid.NewGuid();

            CoreUnit.Repository.Insert(value);
        }

        /// <inheritdoc />
        public void Update(T value)
        {
            if (value == null) throw new ArgumentNullException(nameof(value));
            if (value.Id == null) throw new ArgumentNullException(nameof(value.Id));
            if (CoreUnit == null) throw new NullReferenceException("CoreUnit wasn't initialized");
            if (CoreUnit.Repository == null) throw new NullReferenceException("Repository wasn't initialized");

            CoreUnit.Repository.Update(x => x.Id == value.Id, value);

        }

        /// <inheritdoc />
        public void Delete(Guid id)
        {
            if (CoreUnit == null) throw new NullReferenceException("CoreUnit wasn't initialized");
            if (CoreUnit.Repository == null) throw new NullReferenceException("Repository wasn't initialized");

            CoreUnit.Repository.Delete(x => x.Id == id);
        }

        /// <inheritdoc />
        public void Delete(IEnumerable<Guid> idsToDelete)
        {
            if (idsToDelete == null) throw new ArgumentNullException(nameof(idsToDelete));
            if (!idsToDelete.Any()) return;
            if (CoreUnit == null) throw new NullReferenceException("CoreUnit wasn't initialized");
            if (CoreUnit.Repository == null) throw new NullReferenceException("Repository wasn't initialized");

            CoreUnit.Repository.DeleteMany(x => idsToDelete.Contains(x.Id));
        }
    }
}