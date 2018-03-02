
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using vidley.net.Features.Genres;

namespace vidley.net.Features.Movies
{
    [BsonIgnoreExtraElements]
    public class Movie
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; private set; }

        [Required]
        [BsonElement("title")]
        public string Title { get; set; }

        [Required]
        [BsonElement("genre")]
        public Genre Genre { get; private set; }
        
        [BsonElement("numberInStock")]
        public int NumberInStock { get; set; }

        [BsonElement("dailyRentalRate")]
        public int DailyRentalRate { get; set; }


        public Movie(MovieWriteDTO model, Genre genre)
        {
            Title = model.Title;
            Genre = genre;
            NumberInStock = model.NumberInStock;
            DailyRentalRate = model.DailyRentalRate;
        }
    }
}
