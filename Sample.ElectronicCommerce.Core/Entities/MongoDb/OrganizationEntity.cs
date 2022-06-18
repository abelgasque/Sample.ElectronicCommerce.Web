using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.MongoDB
{
    public class OrganizationEntity
    {
        public OrganizationEntity() { }
        
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

        [BsonElement("base_url")]
        public string BaseUrl { get; set; }

        [BsonElement("base_url_production")]
        public string BaseUrlProduction { get; set; }

        [BsonElement("version")]
        public string Version { get; set; }
    }
}
