using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Models;

namespace TestCase.Repositories
{
    public interface ICategoriesRepository
    {
        // api/categories/[GET]
        Task<IEnumerable<Categories>> GetAllCategories();
        // api/categories/1/[GET]
        Task<Categories> GetCategories(string _id);
        // api/categories/[POST]
        Task Create(Categories categories);
        // api/categories/[PUT]
        Task<bool> Update(Categories categories);
        // api/categories/1/[DELETE]
        Task<bool> Delete(Categories category);
        Task<long> GetNextId();
    }
}
