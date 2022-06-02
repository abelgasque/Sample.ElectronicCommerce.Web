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
        public string LastName { get; set; }

        [BsonElement("image_url")]
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("mail")]
        [JsonProperty("mail")]
        public string Mail { get; set; }

        [BsonElement("nu_cell_phone")]
        [JsonProperty("nuCellPhone")]
        public string NuCellPhone { get; set; }

        [BsonElement("code_desblock")]
        [JsonProperty("codeDesblock")]
        public string CodeDesblock { get; set; }

        [BsonElement("is_block")]
        [JsonProperty("isBlock")]
        public bool IsBlock { get; set; }
    }
}
