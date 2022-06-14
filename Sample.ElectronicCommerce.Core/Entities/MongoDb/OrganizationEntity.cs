using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Sample.ElectronicCommerce.Core.Entities.Base;

namespace Sample.ElectronicCommerce.Core.Entities.MongoDb
{
    public class OrganizationEntity : MongoBaseEntity
    {
        public OrganizationEntity() { }

        [BsonElement("base_url")]
        public string BaseUrl { get; set; }

        [BsonElement("base_url_production")]
        public string BaseUrlProduction { get; set; }

        [BsonElement("version")]
        public string Version { get; set; }
    }
}
