using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using vidley.net.Data;
using vidley.net.Features.Customers;
using vidley.net.Features.Movies;

namespace vidley.net.Features.Rentals
{
    public class CustomerDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("name")]
        [BsonRequired]
        public string Name { get; set; }

        [BsonElement("phone")]
        [BsonRequired]
        public string Phone { get; set; }

        [BsonElement("isGold")]
        [BsonRequired]
        public bool IsGold { get; set; }
    }

    public class MovieDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("title")]
        [BsonRequired]
        public string Title { get; set; }

        [BsonElement("dailyRentalRate")]
        [BsonRequired]
        public decimal DailyRentalRate { get; set; }
    }

    public class Rental: IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; private set; }

        [BsonElement("customer")]
        [BsonRequired]
        public CustomerDTO Customer { get; set; }

        [BsonElement("movie")]
        [BsonRequired]
        public MovieDTO Movie { get; set; }

        [BsonElement("dateRented")]
        [BsonRequired]
        public DateTime DateRented { get; set; }

        [BsonElement("dateReturned")]
        public DateTime? DateReturned { get; set; }

        [BsonElement("rentalFee")]
        public decimal RentalFee { get; set; }

        public Rental(RentalParams rental, Customer customer, Movie movie)
        {
            this.DateRented = rental.DateRented ?? DateTime.Now;
            this.DateReturned = rental.DateReturned;
            this.RentalFee = rental.RentalFee;

            this.Customer = new CustomerDTO
            {
                Id = customer.Id,
                Name = customer.Name,
                Phone = customer.Phone,
                IsGold = customer.IsGold
            };
            this.Movie = new MovieDTO
            {
                Id = movie.Id,
                Title = movie.Title,
                DailyRentalRate = movie.DailyRentalRate
            };
        }
    }
}
