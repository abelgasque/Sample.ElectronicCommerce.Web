using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace Sample.ElectronicCommerce.WebSocket.Entities
{
    public class Message
    {
        public Message() { }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        
        public long IdUser { get; set; } = 0;
        
        public string UserName { get; set; } = null;
        
        public string Text { get; set; } = null;
        
        public DateTime DtCreation { get; set; } = DateTime.Now;
    }
}
