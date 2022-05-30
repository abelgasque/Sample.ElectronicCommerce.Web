using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sample.ElectronicCommerce.BrokerMail.Entities
{
    public class BrokerMailEntity
    {      
		[BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }
		
		[BsonElement("dtCreation")]
		public DateTime? DtCreation { get; set; }

		[BsonElement("dtLastUpdate")]
		public DateTime? DtLastUpdate { get; set; }		

		[BsonElement("server")]
		public string Server { get; set; }

		[BsonElement("name")]
		public string Name { get; set; }

		[BsonElement("password")]
		public string Password { get; set; }

		[BsonElement("userName")]
		public string UserName { get; set; }

		[BsonElement("code")]
		public string Code { get; set; }

		[BsonElement("port")]
		public int? Port { get; set; }

		[BsonElement("isEnabledSsl")]
		public bool IsEnabledSsl { get; set; }

		[BsonElement("isActive")]
		public bool IsActive { get; set; }
	}
}
