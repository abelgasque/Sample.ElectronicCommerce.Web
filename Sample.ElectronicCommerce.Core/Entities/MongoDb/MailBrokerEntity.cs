using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.MongoDb
{
    public class MailBrokerEntity
    {
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
        [BsonElement("server")]
        public string Server { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("user_name")]
        public string UserName { get; set; }

        [BsonElement("port")]
        public int? Port { get; set; }

        [BsonElement("is_enabled_ssl")]
        public bool IsEnabledSsl { get; set; }
    }
}
