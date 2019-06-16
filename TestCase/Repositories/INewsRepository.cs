using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Models;

namespace TestCase.Repositories
{
    public interface INewsRepository
    {
        // api/[GET]
        Task<IEnumerable<News>> GetAllNews();
        // api/1/[GET]
        Task<News> GetNews(long id);
        // api/[POST]
        Task Create(News news);
        // api/[PUT]
        Task<bool> Update(News news);
        // api/1/[DELETE]
        Task<bool> Delete(long id);
        Task<long> GetNextId();
    }
}
