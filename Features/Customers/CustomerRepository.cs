using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using vidley.net.Data;

namespace vidley.net.Features.Customers
{
    public class CustomerRepository: IRepository<Customer>
    {
        private DbContext _context { get; }

        public CustomerRepository(DbContext context)
        {
            _context = context;
        }

        public async Task<Customer> Add(Customer model)
        {
            await _context.Customers.InsertOneAsync(model);

            return model;
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            return await _context.Customers.Find(_ => true).ToListAsync();
        }

        public async Task<Customer> Get(string id)
        {
            return await _context.Customers.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Customer> Remove(string id)
        {
            var model = await Get(id);
            await _context.Customers.DeleteOneAsync(g => g.Id == id);

            return model;
        }

        public async Task<Customer> Update(string id, Customer model)
        {
            await _context.Customers.ReplaceOneAsync(g => g.Id == id, model);
            
            return await Get(id);
        }
    }
}