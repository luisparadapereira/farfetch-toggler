using System;
using MongoDB.Bson.Serialization.Attributes;

namespace Farfetch.Models
{
    public class DbT
    {
        [BsonId]
        public Guid Id { get; set; }
    }
}