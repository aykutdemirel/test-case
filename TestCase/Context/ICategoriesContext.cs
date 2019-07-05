using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Models;

namespace TestCase.Context
{
    public interface ICategoriesContext
    {
        IMongoCollection<Categories> Categories { get; }

    }
}
