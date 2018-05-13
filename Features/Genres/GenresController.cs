
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using vidley.net.Features.Genres;
using System.Linq;
using MongoDB.Bson;
using System.Threading.Tasks;
using vidley.net.Data;
using Microsoft.AspNetCore.Authorization;

namespace vidley.net.Features.Genres
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GenresController: ControllerBase
    {
        private IRepository<Genre> _repository { get; }

        public GenresController(IRepository<Genre> repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Genre>> Get()
        {           
           return await _repository.GetAll(null, g => g.Name);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
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
            await _repository.Add(model);

            return model;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Genre>> Put(string id, Genre model)
        {
            await _repository.Update(id, model);
            var genre = await _repository.Get(id);
            if (genre == null)
                return NotFound("No genre found with the given id");

            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Genre>> Delete(string id)
        {
            var genre = await _repository.Get(id);
            if (genre == null)
                return NotFound("No genre found with the given id");

            await _repository.Remove(id);

            return Ok(genre);
        }
    }
}
