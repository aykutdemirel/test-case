using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using TestCase.Models;
using TestCase.Repositories;

namespace TestCase.Controllers
{
    [Route("api/images")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IImagesRepository _repo;

        public ImagesController(IImagesRepository repo)
        {
            _repo = repo;
        }

        // GET api/images
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Images>>> Get()
        {
            return new ObjectResult(await _repo.GetAllImages());

        }

        // GET api/images/1
        [HttpGet("{_id}")]
        [Consumes("image/jpeg")]
        public async Task<ActionResult> Get(string _id)
        {
            var images = await _repo.GetImages(ObjectId.Parse(_id));
            if (images == null)
                return new NotFoundResult();

            return File(images.PictureDataAsString, "image/jpeg");
        }

        // POST api/images
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] IFormFile file)
        {
            Images images = new Images();

            using (var stream = new System.IO.MemoryStream())
            {
                await file.CopyToAsync(stream);
                images.FileName = file.FileName;
                images.PictureDataAsString = stream.ToArray();
                // Images images = new Images { FileName = file.FileName, PictureDataAsString = stream.ToArray() };
            }

            await _repo.Create(images);

            return new OkObjectResult(images);
        }


    }
}
