using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Models;

namespace TestCase.Repositories
{
    public interface IImagesRepository
    {
        // api/[GET]
        Task<IEnumerable<Images>> GetAllImages();
        // api/1/[GET]
        Task<Images> GetImages(ObjectId _id);
        // api/[POST]
        Task Create(Images images);
        // api/[PUT]
        Task<bool> Update(Images images);
        // api/1/[DELETE]
        Task<bool> Delete(ObjectId _id);
    }
}
