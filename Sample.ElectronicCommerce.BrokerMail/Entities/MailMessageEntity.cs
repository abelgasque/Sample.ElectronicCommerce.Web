using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Sample.ElectronicCommerce.Core.Entities.Base;
using System;

namespace Sample.ElectronicCommerce.BrokerMail.Entities
{
    public class MailMessageEntity : MongoBaseEntity
    {        
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
