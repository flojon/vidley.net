
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using vidley.net.Data;
using vidley.net.Features.Genres;
using Microsoft.AspNetCore.Authorization;

namespace vidley.net.Features.Movies
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MoviesController: ControllerBase
    {
        private IRepository<Movie> _repository { get; }
        private IRepository<Genre> _genreRepository { get; }

        public MoviesController(IRepository<Movie> repository, IRepository<Genre> genreRepository)
        {
            this._repository = repository;
            this._genreRepository = genreRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<Movie>> GetAll(string genreId)
        {
            if (genreId == null)
                return await _repository.GetAll();
            else
                return await _repository.GetAll(m => m.Genre.Id == genreId);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Movie>> Get(string id)
        {
            var movie = await _repository.Get(id);
            if (movie == null)
                return NotFound("No movie found with the given id");

            return Ok(movie);
        }

        [HttpPost]
        public async Task<ActionResult<Movie>> Post(MovieWriteDTO model)
        {
            var genre = await _genreRepository.Get(model.GenreId);
            if (genre == null)
                return BadRequest("No genre found with the given id");

            var movie = new Movie(model, genre);

            await _repository.Add(movie);

            return movie;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> Put(string id, MovieWriteDTO model)
        {
            var genre = await _genreRepository.Get(model.GenreId);
            if (genre == null)
                return BadRequest("No genre found with the given id");

            var movie = new Movie(model, genre);

            await _repository.Update(id, movie);

            movie = await _repository.Get(id);

            if (movie == null)
                return NotFound("No movie found with the given id");

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> Delete(string id)
        {
            var movie = await _repository.Get(id);
            if (movie == null)
                return NotFound("No movie found with the given id");

            await _repository.Remove(id);

            return Ok(movie);
        }
    }
}
