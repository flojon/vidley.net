using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using vidley.net;

namespace vidley.net.Data
{
    public class Repository<T>: IRepository<T>
        where T: IEntity
    {
        private IMongoDatabase _database { get; }
        
        public Repository(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        private string _collectionName = null;
        public string CollectionName
        {
            get
            {
                if (_collectionName != null)
                    return _collectionName;
                
                return typeof(T).Name.ToLower() + "s";
            }

            set
            {
                _collectionName = value;
            }
        }


        public IMongoCollection<T> Collection
        {
            get
            {
                return _database.GetCollection<T>(CollectionName);
            }
        }

        public async Task Add(T model)
        {
            await Collection.InsertOneAsync(model);
        }

        public async Task<IEnumerable<T>> GetAll(Expression<Func<T, object>> sortBy = null, bool sortDescending = false)
        {
            var query = Collection.Find(_ => true);
            if (sortBy != null) {
                if (sortDescending) {
                    query = query.SortByDescending(sortBy);
                } else {
                    query = query.SortBy(sortBy);
                }
            }

            return await query.ToListAsync();
        }

        public async Task<T> Get(string id)
        {
            return await Collection.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task Remove(string id)
        {
            await Collection.DeleteOneAsync(g => g.Id == id);
        }

        public async Task Update(string id, T model)
        {
            await Collection.ReplaceOneAsync(g => g.Id == id, model);
        }

        public async Task<IEnumerable<T>> Find(System.Linq.Expressions.Expression<System.Func<T, bool>> filter)
        {
            return await Collection.Find(filter).ToListAsync();
        }

        public async Task<T> FindOne(System.Linq.Expressions.Expression<System.Func<T, bool>> filter)
        {
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
    }
}
