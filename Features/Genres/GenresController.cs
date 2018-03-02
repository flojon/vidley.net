
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using vidley.net.Features.Genres;
using System.Linq;
using MongoDB.Bson;
using System.Threading.Tasks;
using vidley.net.Data;

namespace vidley.net.Features.Genres
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController: ControllerBase
    {
        private IRepository<Genre> _repository { get; }

        public GenresController(IRepository<Genre> repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Genre>> Get()
        {           
           return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Genre>> Get(string id)
        {
            var genre = await _repository.Get(id);
            if (genre == null)
                return NotFound("No genre found with the given id");

            return Ok(genre);
        }

        [HttpPost]
        public async Task<Genre> Post(Genre model)
        {
            var genre = await _repository.Add(model);

            return genre;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Genre>> Put(string id, Genre model)
        {
            var genre = await _repository.Update(id, model);
            if (genre == null)
                return NotFound("No genre found with the given id");

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Genre>> Delete(string id)
        {
            var genre = await _repository.Remove(id);
            if (genre == null)
                return NotFound("No genre found with the given id");

            return Ok(genre);
        }
    }
}
