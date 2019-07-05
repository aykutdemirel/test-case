using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestCase.Models
{
    public class Categories
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string name { get; set; }
    }
}
