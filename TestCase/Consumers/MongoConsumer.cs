using MassTransit;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Controllers;
using TestCase.Models;
using TestCase.Repositories;

namespace TestCase.Consumers
{
    public class MongoConsumer : IConsumer<News>
    {
        private readonly INewsRepository _repo;
        private readonly IElasticClient _elasticClient;

        public MongoConsumer(INewsRepository repo, IElasticClient elasticClient)
        {
            _repo = repo;
            _elasticClient = elasticClient;
        }

        public async Task Consume(ConsumeContext<News> context)
        {
            var newsCommand = context.Message;
            await Console.Out.WriteAsync($"News Title: {newsCommand.Title} News Content: {newsCommand.Content}");
            newsCommand.Id = await _repo.GetNextId();
            await _repo.GetNextId();
            await _repo.Create((News)newsCommand);
        }
    }
}
