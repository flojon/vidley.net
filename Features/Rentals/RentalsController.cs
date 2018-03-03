using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vidley.net.Data;
using vidley.net.Features.Customers;
using vidley.net.Features.Movies;

namespace vidley.net.Features.Rentals
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController: ControllerBase
    {
        public IRepository<Rental> _rentalRepository { get; }
        public IRepository<Customer> _customerRepository { get; }
        public IRepository<Movie> _movieRepository { get; }

        public RentalsController(IRepository<Rental> rentalRepository, IRepository<Customer> customerRepository, IRepository<Movie> movieRepository)
        {
            this._rentalRepository = rentalRepository;
            this._customerRepository = customerRepository;
            this._movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Rental>> Get()
        {
            return await _rentalRepository.GetAll();
        }

        [HttpPost]
        public async Task<ActionResult<Rental>> Post(RentalParams rentalParams)
        {
            var customer = await _customerRepository.Get(rentalParams.CustomerId);
            if (customer == null)
                return BadRequest("No customer found with the given id");

            var movie = await _movieRepository.Get(rentalParams.MovieId);
            if (movie == null)
                return BadRequest("No movie found with the given id");

            var rental = new Rental(rentalParams, customer, movie);
            await _rentalRepository.Add(rental);

            return Ok(rental);
        }
    }
}