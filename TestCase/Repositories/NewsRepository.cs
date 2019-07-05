using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TestCase.Context;
using TestCase.Models;

namespace TestCase.Repositories
{
    public class NewsRepository : INewsRepository
    {
        private readonly INewsContext _context;
        public NewsRepository(INewsContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<News>> GetAllNews()
        {
            return await _context
                            .News
                            .Find(_ => true)
                            .ToListAsync();
        }
        public Task<News> GetNews(string _id)
        {
            FilterDefinition<News> filter = Builders<News>.Filter.Eq(m => m._id, ObjectId.Parse(_id));
            return _context
                    .News
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<News>> GetNewsByTypeId(int type_id)
        {
            FilterDefinition<News> filter = Builders<News>.Filter.Where(m => m.type == type_id);
            return await _context
                    .News
                    .Find(filter)
                    .ToListAsync();
        }

        public async Task<IEnumerable<News>> GetNewsByQuery(string query, int page, int pageSize)
        {
            // FilterDefinition<News> filter = Builders<News>.Filter.Regex("title", BsonRegularExpression.Create(new Regex(query, RegexOptions.IgnoreCase)));
            var filter = new BsonDocument { { "title", new BsonDocument { { "$regex", query }, { "$options", "$i" } } } };

            return await _context
                    .News
                    .Find(filter)
                    .ToListAsync();
        }

        public async Task Create(News news)
        {
            await _context.News.InsertOneAsync(news);
        }
        public async Task<bool> Update(News news)
        {
            ReplaceOneResult updateResult =
                await _context
                        .News
                        .ReplaceOneAsync(
                            filter: g => g.Id == news.Id,
                            replacement: news);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(string _id)
        {
            FilterDefinition<News> filter = Builders<News>.Filter.Eq(m => m._id, ObjectId.Parse(_id));
            DeleteResult deleteResult = await _context
                                                .News
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
        public async Task<long> GetNextId()
        {
            return await _context.News.CountDocumentsAsync(new BsonDocument()) + 1;
        }
    }
}
