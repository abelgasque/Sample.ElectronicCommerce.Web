using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sample.ElectronicCommerce.BrokerMail.Entities
{
    public class MailMessageEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

		[BsonElement("idMail")]
        public string IdMail { get; set; }
        
        [BsonElement("idMailBroker")]
        public string IdMailBroker { get; set; }

		[BsonElement("idUser")]
        public string IdUser { get; set; }
		
        [BsonElement("dtCreation")]
        public DateTime? DtCreation { get; set; }

		[BsonElement("dtLastUpdate")]
        public DateTime? DtLastUpdate { get; set; }        

		[BsonElement("dtSentBroker")]
        public DateTime? DtSentBroker { get; set; }

		[BsonElement("body")]        
        public string Body { get; set; }
        
        [BsonElement("mail")]
        public string Mail { get; set; }
        
        [BsonElement("title")]
        public string Title { get; set; }
        
        [BsonElement("version")]
        public string Version { get; set; }

		[BsonElement("wasSentBroker")]
        public bool WasSentBroker { get; set; }

		[BsonElement("isFree")]
        public bool IsFree { get; set; }

		[BsonElement("isPriority")]
        public bool IsPriority { get; set; }

		[BsonElement("isSuccess")]
        public bool IsSuccess { get; set; }

		[BsonElement("isTest")]
        public bool IsTest { get; set; }

		[BsonElement("isActive")]
        public bool IsActive { get; set; }
    }
}
