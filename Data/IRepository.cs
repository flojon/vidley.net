using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace vidley.net.Data
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(string id);
        Task<T> Add(T model);
        Task<T> Update(string id, T model);
        Task<T> Remove(string id);
    }    
}
