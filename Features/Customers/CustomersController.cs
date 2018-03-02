
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using vidley.net.Features.Customers;
using System.Linq;
using MongoDB.Bson;
using System.Threading.Tasks;
using vidley.net.Data;

namespace vidley.net.Features.Customers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController: ControllerBase
    {
        private IRepository<Customer> _repository { get; }

        public CustomersController(IRepository<Customer> repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Customer>> Get()
        {           
           return await _repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> Get(string id)
        {
            var customer = await _repository.Get(id);
            if (customer == null)
                return NotFound("No customer found with the given id");

            return Ok(customer);
        }

        [HttpPost]
        public async Task<Customer> Post(Customer model)
        {
            await _repository.Add(model);

            return model;
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Customer>> Put(string id, Customer model)
        {
            await _repository.Update(id, model);
            var customer = await Get(id);
            if (customer == null)
                return NotFound("No customer found with the given id");

            return Ok(customer);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Customer>> Delete(string id)
        {
            var customer = await Get(id);
            if (customer == null)
                return NotFound("No customer found with the given id");

            await _repository.Remove(id);

            return Ok(customer);
        }
    }
}
