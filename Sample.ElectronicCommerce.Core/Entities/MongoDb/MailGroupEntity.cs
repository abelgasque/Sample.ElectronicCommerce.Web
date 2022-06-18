using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.MongoDB
{
    public class MailGroupEntity
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
        [BsonElement("body")]
        public string Body { get; set; }

        [BsonElement("title")]
        public string Title { get; set; }

        [BsonElement("vl_mail_unit")]
        public decimal VlMailUnit { get; set; }

        [BsonElement("vl_mail_mass")]
        public decimal VlMailMass { get; set; }

        [BsonElement("is_priority")]
        public bool IsPriority { get; set; }

        [BsonElement("brokers")]
        public ICollection<MailBrokerEntity> Brokers { get; set; }
    }
}
