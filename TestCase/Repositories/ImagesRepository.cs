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
    public class ImagesRepository : IImagesRepository
    {
        private readonly IImagesContext _context;
        public ImagesRepository(IImagesContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Images>> GetAllImages()
        {
            return await _context
                            .Images
                            .Find(_ => true)
                            .ToListAsync();
        }
        public Task<Images> GetImages(ObjectId _id)
        {
            FilterDefinition<Images> filter = Builders<Images>.Filter.Eq(m => m._id, _id);
            return _context
                    .Images
                    .Find(filter)
                    .FirstOrDefaultAsync();
        }
        public async Task Create(Images images)
        {
            await _context.Images.InsertOneAsync(images);
        }
        public async Task<bool> Update(Images images)
        {
            ReplaceOneResult updateResult =
                await _context
                        .Images
                        .ReplaceOneAsync(
                            filter: g => g._id == images._id,
                            replacement: images);
            return updateResult.IsAcknowledged
                    && updateResult.ModifiedCount > 0;
        }
        public async Task<bool> Delete(ObjectId _id)
        {
            FilterDefinition<Images> filter = Builders<Images>.Filter.Eq(m => m._id, _id);
            DeleteResult deleteResult = await _context
                                                .Images
                                              .DeleteOneAsync(filter);
            return deleteResult.IsAcknowledged
                && deleteResult.DeletedCount > 0;
        }
    }
}
