using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Farfetch.Models
{
    public class DbT
    {
        /// <summary>
        /// @todo: Move this a layer up. We can't be sharing references to Mongo in case we drop it.
        /// </summary>
        [BsonId]
        public Guid Id { get; set; }
    }
}