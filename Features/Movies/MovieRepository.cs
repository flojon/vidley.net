using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using vidley.net.Data;

namespace vidley.net.Features.Movies
{
    public class MovieRepository: IRepository<Movie>
    {
        private DbContext _context { get; }

        public MovieRepository(DbContext context)
        {
            _context = context;
        }

        public async Task Add(Movie model)
        {
            await _context.Movies.InsertOneAsync(model);
        }

        public async Task<IEnumerable<Movie>> GetAll()
        {
            return await _context.Movies.Find(_ => true).ToListAsync();
        }

        public async Task<Movie> Get(string id)
        {
            return await _context.Movies.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task Remove(string id)
        {
            await _context.Movies.DeleteOneAsync(g => g.Id == id);
        }

        public async Task Update(string id, Movie model)
        {
            await _context.Movies.ReplaceOneAsync(g => g.Id == id, model);
        }
    }
}