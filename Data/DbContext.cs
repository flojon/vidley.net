using Microsoft.Extensions.Options;
using MongoDB.Driver;
using vidley.net.Features.Genres;
using vidley.net;


namespace vidley.net.Data
{
    public class DbContext
    {
        private IMongoDatabase _database { get; }

        public DbContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Genre> Genres
        {
            get => _database.GetCollection<Genre>("genres");
        }
    }
}