using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using vidley.net.Data;

namespace vidley.net.Features.Genres
{
    public class GenreRepository: IRepository<Genre>
    {
        private DbContext _context { get; }

        public GenreRepository(DbContext context)
        {
            _context = context;
        }

        public async Task Add(Genre model)
        {
            await _context.Collection<Genre>().InsertOneAsync(model);
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Collection<Genre>().Find(_ => true).ToListAsync();
        }

        public async Task<Genre> Get(string id)
        {
            return await _context.Genres.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task Remove(string id)
        {
            await _context.Genres.DeleteOneAsync(g => g.Id == id);
        }

        public async Task Update(string id, Genre model)
        {
            await _context.Genres.ReplaceOneAsync(g => g.Id == id, model);
        }
    }
}