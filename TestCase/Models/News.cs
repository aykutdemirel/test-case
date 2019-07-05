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
        public ObjectId _id { get; set; }
        public long Id { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public string content { get; set; }
        public string[] tags { get; set; }
        public Boolean isPublished  { get; set; }
        public int readCount { get; set; }
        public string category { get; set; }
        public string image { get; set; }
        public int type { get; set; }

    }

}
