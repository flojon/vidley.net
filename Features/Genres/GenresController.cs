
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using vidley.net.Features.Genres;
using System.Linq;

namespace vidley.net.Features.Genres
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController: ControllerBase
    {
        static IList<GenreModel> Genres { get; set; }

        static GenresController()
        {
            Genres = new List<GenreModel>();
            Genres.Add(new GenreModel(1, "Romance"));
            Genres.Add(new GenreModel(2, "Sci-fi"));
            Genres.Add(new GenreModel(3, "Drama"));
            Genres.Add(new GenreModel(4, "Adventure"));
            Genres.Add(new GenreModel(5, "Comedy"));
        }

        [HttpGet]
        public IEnumerable<GenreModel> Get()
        {           
           return Genres;
        }

        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var genre = Genres.SingleOrDefault(g => g.Id == id);
            if (genre == null)
                return NotFound("No genre found with the given id");

            return Ok(genre);
        }

        [HttpPost]
        public GenreModel Post(GenreModel model)
        {
            model.Id = Genres.Max(g => g.Id) + 1;
            Genres.Add(model);

            return model;
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, GenreModel model)
        {
            var genre = Genres.SingleOrDefault(g => g.Id == id);
            if (genre == null)
                return NotFound("No genre found with the given id");

            Genres.Remove(genre);
            model.Id = id;
            Genres.Add(model);

            return Ok(model);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var genre = Genres.SingleOrDefault(g => g.Id == id);
            if (genre == null)
                return NotFound("No genre found with the given id");

            Genres.Remove(genre);

            return Ok(genre);
        }
    }
}
