using System.Collections.Generic;
using System.Threading.Tasks;

namespace vidley.net.Data
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(string id);
        Task Add(T model);
        Task Update(string id, T model);
        Task Remove(string id);
        Task<IEnumerable<T>> Find(System.Linq.Expressions.Expression<System.Func<T, bool>> filter);
    }    
}
