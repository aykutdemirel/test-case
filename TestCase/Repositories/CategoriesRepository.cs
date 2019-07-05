using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Context;
using TestCase.Models;

namespace TestCase.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly ICategoriesContext _context;
        public CategoriesRepository(ICategoriesContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Categories>> GetAllCategories()
        {
            return await _context
                            .Categories
                            .Find(_ => true)
                            .ToListAsync();
        }
        public Task<Categories> GetCategories(string _id)
        {
            FilterDefinition<Categories> filter = Builders<Categories>.Filter.Eq(m => m._id, ObjectId.Parse(_id));
            return _context
                    .Categories
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }

        public async Task Create(Categories categories)
        {
            await _context.Categories.InsertOneAsync(categories);
        }
        public async Task<bool> Update(Categories categories)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Categories
                        .ReplaceOneAsync(
                            filter: g => g._id == categories._id,
                            replacement: categories);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(Categories category)
        {
            FilterDefinition<Categories> filter = Builders<Categories>.Filter.Eq(m => m._id, category._id);
            DeleteResult deleteResult = await _context
                                                .Categories
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public async Task<long> GetNextId()
        {
            return await _context.Categories.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}
