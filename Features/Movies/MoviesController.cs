
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using vidley.net.Data;
using vidley.net.Features.Genres;

namespace vidley.net.Features.Movies
{
    [Route("api/[controller]")]
    [ApiController]
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
        public async Task<IEnumerable<Movie>> Get()
        {           
           return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Movie>> Get(string id)
        {
            var movie = await _repository.Get(id);
            if (movie == null)
                return NotFound("No movie found with the given id");

            return Ok(movie);
        }

        [HttpPost]
        public async Task<Movie> Post(MovieWriteDTO model)
        {
            var genre = await _genreRepository.Get(model.GenreId);
            var movie = new Movie(model, genre);

            await _repository.Add(movie);

            return movie;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Movie>> Put(string id, MovieWriteDTO model)
        {
            var genre = await _genreRepository.Get(model.GenreId);
            var movie = new Movie(model, genre);

            await _repository.Update(id, movie); // TODO check if movie exist?
            if (movie == null)
                return NotFound("No movie found with the given id");

            return Ok(movie);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Movie>> Delete(string id)
        {
            var movie = await Get(id);
            if (movie == null)
                return NotFound("No movie found with the given id");

            await _repository.Remove(id);

            return Ok(movie);
        }
    }
}
