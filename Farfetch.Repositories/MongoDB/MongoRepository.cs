using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Farfetch.DB;
using Farfetch.Models;
using MongoDB.Driver;

namespace Farfetch.Repositories.MongoDB
{
    /// <inheritdoc />
    /// <summary>
    /// Defines a MongoDatabase
    /// </summary>
    public class MongoRepository<T> : IRepository<T> where T: DbT
    {
        /// <summary>
        /// The collection matching T
        /// </summary>
        private readonly IMongoCollection<T> _collection;

        /// <summary>
        /// Default construtor that initializes the database
        /// </summary>
        /// <param name="database">The database information</param>
        public MongoRepository(IDatabase<IMongoDatabase> database)
        {
            if (database == null) throw new ArgumentNullException(nameof(database));
            IMongoDatabase db = database.GetDatabase();
            if (db == null) throw new NullReferenceException("Failed to get the database");

            _collection = db.GetCollection<T>(typeof(T).FullName);
        }

        /// <inheritdoc />
        public List<T> GetAll()
        {
            if (_collection == null) throw new NullReferenceException("Failed to initialize the collection");
            return _collection.AsQueryable()?.ToList();
        }

        /// <inheritdoc />
        public List<T> GetAllFiltered(Expression<Func<T, bool>> expression)
        {
            if (_collection == null) throw new NullReferenceException("Failed to initialize the collection");
            return _collection.Find(expression).ToList();
        }

        /// <inheritdoc />
        public T GetSingle(Expression<Func<T, bool>> expression)
        {
            if (_collection == null) throw new NullReferenceException("Failed to initialize the collection");
            try
            {
                return _collection.Find(expression)?.ToList()?.FirstOrDefault();
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <inheritdoc />
        public void Insert(T value)
        {
            if (_collection == null) throw new NullReferenceException("Failed to initialize the collection");
            _collection.InsertOne(value);
        }

        /// <inheritdoc />
        public void Update(T value)
        {
            if (_collection == null) throw new NullReferenceException("Failed to initialize the collection");
            _collection.ReplaceOne(x => x.Id == value.Id, value);
        }

        /// <inheritdoc />
        public void Update(Expression<Func<T, bool>> expression, T value)
        {
            if (_collection == null) throw new NullReferenceException("Failed to initialize the collection");
            _collection.ReplaceOne(expression, value);
        }

        /// <inheritdoc />
        public void Delete(Expression<Func<T, bool>> expression)
        {
            if (_collection == null) throw new NullReferenceException("Failed to initialize the collection");
            _collection.DeleteOne(expression);
        }

        /// <inheritdoc />
        public void DeleteMany(Expression<Func<T, bool>> expression)
        {
            if (_collection == null) throw new NullReferenceException("Failed to initialize the collection");
            _collection.DeleteMany(expression);
        }
    }
}