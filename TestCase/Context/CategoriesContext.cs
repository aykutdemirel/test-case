using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Models;

namespace TestCase.Context
{
    public class CategoriesContext : ICategoriesContext
    {
        private readonly IMongoDatabase _db;
        public CategoriesContext(Config.MongoDBConfig config)
        {
            var client = new MongoClient(config.ConnectionString);
            _db = client.GetDatabase(config.Database);
        }
        public IMongoCollection<Categories> Categories => _db.GetCollection<Categories>("Categories");
    }
}
