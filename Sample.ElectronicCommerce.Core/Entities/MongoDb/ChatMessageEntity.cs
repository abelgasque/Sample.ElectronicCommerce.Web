using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Sample.ElectronicCommerce.Core.Entities.Base;

namespace Sample.ElectronicCommerce.Core.Entities.MongoDb
{
    public class ChatMessageEntity : MongoBaseEntity
    {
        public ChatMessageEntity() { }

        [BsonElement("id_user_sender")]
        public string IdUserSender { get; set; } = null;

        [BsonElement("id_user_destinatary")]
        public string IdUserDestinatary { get; set; } = null;

        [BsonElement("message")]
        public string Message { get; set; } = null;
    }
}
