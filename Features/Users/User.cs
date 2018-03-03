using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using vidley.net.Data;

namespace vidley.net.Features.Users
{
    public class User: IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonIgnoreIfDefault]
        public string Id { get; private set; }

        [BsonRequired]
        [BsonElement("name")]
        public string Name { get; set; }

        [BsonRequired]
        [BsonElement("email")]
        public string Email { get; set; }

        [BsonRequired]
        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("isAdmin")]
        public bool IsAdmin { get; set; }

        [BsonIgnoreIfDefault]
        [BsonElement("__v")]
        public int Version { get; set; }
    }
}
