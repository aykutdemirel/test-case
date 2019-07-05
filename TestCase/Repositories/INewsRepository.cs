using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Models;

namespace TestCase.Repositories
{
    public interface INewsRepository
    {
        // api/news/[GET]
        Task<IEnumerable<News>> GetAllNews();
        // api/news/1/[GET]
        Task<News> GetNews(string _id);
        // api/news/type/1/[GET]
        Task<IEnumerable<News>> GetNewsByTypeId(int type_id);
        //api/news/search/aegagea
        Task<IEnumerable<News>> GetNewsByQuery(string query, int page, int pageSize);
        // api/news/[POST]
        Task Create(News news);
        // api/news/[PUT]
        Task<bool> Update(News news);
        // api/news/1/[DELETE]
        Task<bool> Delete(string _id);
        Task<long> GetNextId();
    }
}
