using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestCase.Models;
using TestCase.Repositories;

namespace TestCase.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesRepository _repo;

        public CategoriesController(ICategoriesRepository repo)
        {
            _repo = repo;
        }

        // GET api/categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categories>>> Get()
        {
            return new ObjectResult(await _repo.GetAllCategories());

        }

        // GET api/categories/1
        [HttpGet("{_id}")]
        public async Task<ActionResult<Categories>> Get(string _id)
        {
            var categories = await _repo.GetCategories(_id);
            if (categories == null)
                return new NotFoundResult();

            return new ObjectResult(categories);
        }

        // POST api/categories
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Categories categories)
        {
            return new OkObjectResult(_repo.Create(categories));
        }


    }
}