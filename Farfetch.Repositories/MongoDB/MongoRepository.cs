using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Farfetch.DB.MongoDB;
using Farfetch.Models;
using MongoDB.Driver;

namespace Farfetch.Repositories.MongoDB
{
    public class MongoRepository<T> : IRepository<T> where T: DbT
    {
        private readonly IMongoCollection<T> _collection;

        public MongoRepository(MongoDatabase<IMongoDatabase> database)
        {
            if (database == null) throw new ArgumentNullException(nameof(database));
            IMongoDatabase db = database.GetDatabase();
            if (db == null) throw new NullReferenceException("Failed to get the database");

            _collection = db.GetCollection<T>(typeof(T).FullName);
        }

        /// <inheritdoc />
        public List<T> GetAll()
        {
            return _collection?.AsQueryable()?.ToList();
        }

        /// <inheritdoc />
        public List<T> GetAllFiltered(Expression<Func<T, bool>> expression)
        {
            return _collection?.Find(expression).ToList();
        }

        /// <inheritdoc />
        public T GetSingle(Expression<Func<T, bool>> expression)
        {
            return _collection.Find(expression).ToList().FirstOrDefault();
        }

        /// <inheritdoc />
        public void Insert(T value)
        {
            _collection.InsertOne(value);
        }

        /// <inheritdoc />
        public void Update(T value)
        {
            _collection.ReplaceOne(x => x.Id == value.Id, value);
        }

        /// <inheritdoc />
        public void Update(Expression<Func<T, bool>> expression, T value)
        {
            _collection.ReplaceOne(expression, value);
        }

        /// <inheritdoc />
        public void Delete(Expression<Func<T, bool>> expression)
        {
            _collection.DeleteOne(expression);
        }

        /// <inheritdoc />
        public void DeleteMany(Expression<Func<T, bool>> expression)
        {
            _collection.DeleteMany(expression);
        }
    }
}