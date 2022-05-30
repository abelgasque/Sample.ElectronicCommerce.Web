using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Sample.ElectronicCommerce.BrokerChat.Entities
{
    public class BrokerChatEntity
    {
        public BrokerChatEntity() { }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; } = null;
        
        [BsonElement("idUser")]
        public string IdUser { get; set; } = null;
        
        [BsonElement("userName")]
        public string UserName { get; set; } = null;
        
        [BsonElement("text")]
        public string Text { get; set; } = null;
        
        [BsonElement("dtCreation")]
        public DateTime DtCreation { get; set; } = DateTime.Now;
    }
}
