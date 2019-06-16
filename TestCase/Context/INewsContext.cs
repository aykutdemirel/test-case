using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;
using TestCase.Models;

namespace TestCase.Context
{
    public interface INewsContext
    {
        IMongoCollection<News> News { get; }
    }

}
