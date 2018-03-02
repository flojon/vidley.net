using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using vidley.net.Data;

namespace vidley.net.Features.Customers
{
    [BsonIgnoreExtraElements]
    public class Customer: IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; private set; }
        
        [Required]
        [BsonElement("name")]
        public string Name { get; set; }

        [Required]
        [BsonElement("phone")]
        public string Phone { get; set; }

        [BsonElement("isGold")]
        public bool IsGold { get; set; }
    }
}