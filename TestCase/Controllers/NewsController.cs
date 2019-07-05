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
using Microsoft.AspNetCore.Http;
using MongoDB.Bson;

namespace TestCase.Controllers {

    [Route("api/news")]
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

        // GET api/news/search
        [HttpGet("search")]
        public async Task<ActionResult<IEnumerable<News>>> Find(string query, int page = 1, int pageSize = 10)
        {

            var news = await _repo.GetNewsByQuery(query, page, pageSize);

            if (news == null)
                return new NotFoundResult();

            return new OkObjectResult(news);

        }

        // GET api/news/1
        [HttpGet("{_id}")]
        public async Task<ActionResult<News>> Get(string _id)
        {
            var news = await _repo.GetNews(_id);
            if (news == null)
                return new NotFoundResult();

            return new ObjectResult(news);
        }

        [HttpGet("type/{type_id}")]
        public async Task<ActionResult<News>> GetNewsByType(int type_id)
        {
            var news = await _repo.GetNewsByTypeId(type_id);
            if (news == null)
                return new NotFoundResult();

            return new ObjectResult(news);
        }

        // POST api/news
        [HttpPost]
        public async Task<ActionResult> Post([FromBody]News news)
        {
            // send to rabbitmq
            await _busControl.Publish(news);
            return new OkResult();
        }

        // PUT api/news/1
        [HttpPut("{_id}")]
        public async Task<ActionResult<News>> Put(string _id, [FromBody] News news)
        {
            var newsFromDb = await _repo.GetNews(_id);
            if (newsFromDb == null)
                return new NotFoundResult();
            news.Id = newsFromDb.Id;
            news._id = newsFromDb._id;
            await _repo.Update(news);
            return new OkObjectResult(news);
        }
        // DELETE api/news/1
        [HttpDelete("{_id}")]
        public async Task<IActionResult> Delete(string _id)
        {
            var news = await _repo.GetNews(_id);
            if (news == null)
                return new NotFoundResult();
            await _repo.Delete(_id);
            return new OkResult();
        }

    }
}
