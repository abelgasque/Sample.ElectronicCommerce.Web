using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sample.ElectronicCommerce.BrokerMail.Entities 
{
    public class MailEntity
    {
		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

		[BsonElement("dtCreation")]
		public DateTime DtCreation { get; set; }

		[BsonElement("dtLastUpdate")]
		public DateTime? DtLastUpdate { get; set; }

		[BsonElement("body")]
		public string Body { get; set; }
		
		[BsonElement("title")]
		public string Title { get; set; }

		[BsonElement("name")]
		public string Name { get; set; }

		[BsonElement("code")]
		public string Code { get; set; }

		[BsonElement("vlMailUnit")]
		public decimal VlMailUnit { get; set; }

		[BsonElement("vlMailMass")]
		public decimal VlMailMass { get; set; }

		[BsonElement("isPriority")]
		public bool IsPriority { get; set; }

		[BsonElement("isActive")]
		public bool IsActive { get; set; }

		[BsonElement("brokers")]
		public ICollection<BrokerMailEntity> Brokers { get; set; }
	}
}
