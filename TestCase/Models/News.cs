using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace TestCase.Models
{
    public class News
    {
        [BsonId]
        public ObjectId InternalId { get; set; }
        public long Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string[] Tags { get; set; }
        public Boolean IsPublished  { get; set; }
        public int ReadCount { get; set; }

    }

}
