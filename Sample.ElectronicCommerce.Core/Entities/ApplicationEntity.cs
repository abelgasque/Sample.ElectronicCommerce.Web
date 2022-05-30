using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sample.ElectronicCommerce.Core.Entities
{
    public class ApplicationEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("dtCreation")]
        public DateTime? DtCreation { get; set; }

        [BsonElement("dtLastUpdate")]
        public DateTime? DtLastUpdate { get; set; }

        [BsonElement("baseUrl")]
        public string BaseUrl { get; set; }

        [BsonElement("baseUrlProduction")]
        public string BaseUrlProduction { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("version")]
        public string Version { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }
    }
}
