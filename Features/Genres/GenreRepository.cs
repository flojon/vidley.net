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

        public async Task<Genre> Add(Genre model)
        {
            await _context.Genres.InsertOneAsync(model);

            return model;
        }

        public async Task<IEnumerable<Genre>> GetAll()
        {
            return await _context.Genres.Find(_ => true).ToListAsync();
        }

        public async Task<Genre> Get(string id)
        {
            return await _context.Genres.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Genre> Remove(string id)
        {
            var model = await Get(id);
            await _context.Genres.DeleteOneAsync(g => g.Id == id);

            return model;
        }

        public async Task<Genre> Update(string id, Genre model)
        {
            model.Id = id;
            await _context.Genres.ReplaceOneAsync(g => g.Id == id, model);
            //model.Id = id;

            return model;
        }
    }
}