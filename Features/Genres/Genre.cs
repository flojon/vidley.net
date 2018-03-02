using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace vidley.net.Features.Genres
{
    [BsonIgnoreExtraElements]
    public class Genre
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; private set; }
        
        [Required]
        [BsonElement("name")]
        public string Name { get; set; }
    }
}
