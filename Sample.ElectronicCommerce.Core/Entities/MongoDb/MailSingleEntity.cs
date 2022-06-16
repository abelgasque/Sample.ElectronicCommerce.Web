using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.MongoDb
{
    public class MailSingleEntity
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
         
		[BsonElement("id_mail")]
        public string IdMail { get; set; }
        
        [BsonElement("id_mail_broker")]
        public string IdMailBroker { get; set; }

		[BsonElement("id_user")]
        public string IdUser { get; set; }
		
     	[BsonElement("dt_sent_broker")]
        public DateTime? DtSentBroker { get; set; }

		[BsonElement("body")]        
        public string Body { get; set; }
        
        [BsonElement("mail")]
        public string Mail { get; set; }
        
        [BsonElement("title")]
        public string Title { get; set; }
        
        [BsonElement("version")]
        public string Version { get; set; }

		[BsonElement("was_sent_broker")]
        public bool WasSentBroker { get; set; }

		[BsonElement("is_free")]
        public bool IsFree { get; set; }

		[BsonElement("is_priority")]
        public bool IsPriority { get; set; }

		[BsonElement("is_success")]
        public bool IsSuccess { get; set; }

		[BsonElement("is_test")]
        public bool IsTest { get; set; }
    }
}
