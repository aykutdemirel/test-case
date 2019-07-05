using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestCase.Models
{
    public class Images
    {
        [BsonId]
        public ObjectId _id { get; set; }
        public string FileName { get; set; }
        public byte[] PictureDataAsString { get; set; }
    }
}
