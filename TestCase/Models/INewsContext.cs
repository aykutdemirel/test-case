using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace TestCase.Models
{
    public interface INewsContext
    {
        IMongoCollection<News> News { get; }
    }

}
