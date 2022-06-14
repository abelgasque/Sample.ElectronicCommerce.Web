using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Sample.ElectronicCommerce.Core.Entities.Base;

namespace Sample.ElectronicCommerce.Security.Entities
{
    public class UserEntity : MongoBaseEntity
    {
        public UserEntity() { }

        [BsonElement("last_name")]
        [JsonProperty("lastName")]
        public string LastName { get; set; } = null;

        [BsonElement("image_url")]
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; } = null;

        [BsonElement("mail")]
        [JsonProperty("mail")]
        public string Mail { get; set; } = null;

        [BsonElement("nu_cell_phone")]
        [JsonProperty("nuCellPhone")]
        public string NuCellPhone { get; set; } = null;
    }
}
