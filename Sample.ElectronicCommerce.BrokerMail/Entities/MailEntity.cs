using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Sample.ElectronicCommerce.Core.Entities.Base;

namespace Sample.ElectronicCommerce.BrokerMail.Entities 
{
    public class MailEntity : MongoBaseEntity
	{
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
