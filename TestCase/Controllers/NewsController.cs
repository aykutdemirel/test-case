using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TestCase.Models;
using TestCase.Repositories;
using Confluent.Kafka;
using MassTransit;
using TestCase.Config;
using System.Configuration;
using Nest;

namespace TestCase.Controllers {

    [Route("api/[controller]")]
    [ApiController]
    public class NewsController : ControllerBase
    {
        private readonly INewsRepository _repo;
        private readonly IBusControl _busControl;
        private readonly IElasticClient _elasticClient;

        public NewsController(INewsRepository repo, IElasticClient elasticClient, IBusControl busControl)
        {
            _repo = repo;
            _busControl = busControl;
            _elasticClient = elasticClient;
        }
        // GET api/news
        [HttpGet]
        public async Task<ActionResult<IEnumerable<News>>> Get()
        {
            return new ObjectResult(await _repo.GetAllNews());
        }

        [HttpGet("/search")]
        public async Task<ActionResult<IEnumerable<News>>> Find(string query, int page = 1, int pageSize = 10)
        {
            var response = await _elasticClient.SearchAsync<News>(
                s => s.Query(q => q.QueryString(d => d.Query(query)))
                    .From((page - 1) * pageSize)
                    .Size(pageSize));
            return new ObjectResult(response.Documents);
        }

        // GET api/news/1
        [HttpGet("{id}")]
        public async Task<ActionResult<News>> Get(long id)
        {
            var news = await _repo.GetNews(id);
            if (news == null)
                return new NotFoundResult();

            return new ObjectResult(news);
        }

        [HttpGet("{id}/test")]
        public string GetTest()
        {
            return "{test:'testtestse'}";
        }

        // POST api/news
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] News news)
        {
            // send to rabbitmq
            await _busControl.Publish(news);
            return new OkResult();
        }

        // PUT api/news/1
        [HttpPut("{id}")]
        public async Task<ActionResult<News>> Put(long id, [FromBody] News news)
        {
            var newsFromDb = await _repo.GetNews(id);
            if (newsFromDb == null)
                return new NotFoundResult();
            news.Id = newsFromDb.Id;
            news.InternalId = newsFromDb.InternalId;
            await _repo.Update(news);
            return new OkObjectResult(news);
        }
        // DELETE api/news/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var news = await _repo.GetNews(id);
            if (news == null)
                return new NotFoundResult();
            await _repo.Delete(id);
            return new OkResult();
        }

    }
}
