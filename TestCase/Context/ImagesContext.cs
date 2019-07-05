using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Models;

namespace TestCase.Context
{
    public class ImagesContext : IImagesContext
    {
        private readonly IMongoDatabase _db;
        public ImagesContext(Config.MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<Images> Images => _db.GetCollection<Images>("Images");
    }
}
