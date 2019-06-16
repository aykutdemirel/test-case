using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase;
using MongoDB.Driver;
using TestCase.Models;

namespace TestCase.Context
{
    public class NewsContext : INewsContext
    {
        private readonly IMongoDatabase _db;
        public NewsContext(Config.MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<News> News => _db.GetCollection<News>("News");
    }

}
