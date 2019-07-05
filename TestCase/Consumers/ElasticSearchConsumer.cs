using MassTransit;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestCase.Models;
using TestCase.Repositories;

namespace TestCase.Consumers
{
    public class ElasticSearchConsumer : IConsumer<News>
    {
        private readonly INewsRepository _repo;
        private readonly IElasticClient _elasticClient;

        public ElasticSearchConsumer(INewsRepository repo, IElasticClient elasticClient)
        {
            _repo = repo;
            _elasticClient = elasticClient;
        }

        public async Task Consume(ConsumeContext<News> context)
        {
            var newsCommand = context.Message;
            await Console.Out.WriteAsync($"News Title: {newsCommand.title} News Content: {newsCommand.content}");
            newsCommand.Id = await _repo.GetNextId();
            await _elasticClient.IndexDocumentAsync((News)newsCommand);
        }
    }
}
