using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.MongoDB
{
    public class ChatMessageEntity
    {
        public ChatMessageEntity() { }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonElement("code")]
        [JsonProperty("code")]
        public string Code { get; set; } = null;

        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; } = null;

        [BsonElement("dt_creation")]
        [JsonProperty("dtCreation")]
        public DateTime? DtCreation { get; set; } = null;

        [BsonElement("dt_last_update")]
        [JsonProperty("dtLastUpdate")]
        public DateTime? DtLastUpdate { get; set; } = null;

        [BsonElement("is_active")]
        [JsonProperty("isActive")]
        public bool IsActive { get; set; } = true;

        [BsonElement("id_user_sender")]
        public string IdUserSender { get; set; } = null;

        [BsonElement("id_user_destinatary")]
        public string IdUserDestinatary { get; set; } = null;

        [BsonElement("message")]
        public string Message { get; set; } = null;
    }
}
