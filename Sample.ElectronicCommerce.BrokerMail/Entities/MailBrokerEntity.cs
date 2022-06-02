using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Sample.ElectronicCommerce.Core.Entities.Base;

namespace Sample.ElectronicCommerce.BrokerMail.Entities
{
    public class MailBrokerEntity : MongoBaseEntity
	{      		
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
