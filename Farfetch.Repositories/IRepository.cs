using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Farfetch.Models;

namespace Farfetch.Repositories
{
    public interface IRepository<T> where T: DbT
    {
        List<T> GetAll();

        List<T> GetAllFiltered(Expression<Func<T, bool>> expression);

        T GetSingle(Expression<Func<T, bool>> expression);

        void Insert(T value);

        void Update(Expression<Func<T, bool>> expression, T value);

        void Delete(Expression<Func<T, bool>> expression);

        void DeleteMany(Expression<Func<T, bool>> expression);

    }
}