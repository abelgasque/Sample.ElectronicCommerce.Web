using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sample.ElectronicCommerce.Security.Entities
{
    public class UserRoleEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("code")]
        public string Code { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }
    }
}
