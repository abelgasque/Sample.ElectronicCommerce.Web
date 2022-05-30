using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace Sample.ElectronicCommerce.Security.Entities
{
    public class UserEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("dtCreation")]
        public DateTime? DtCreation { get; set; }

        [BsonElement("dtLastUpdate")]
        public DateTime? DtLastUpdate { get; set; }

        [BsonElement("dtLastBlock")]
        public DateTime? DtLastBlock { get; set; }

        [BsonElement("dtLastDesblock")]
        public DateTime? DtLastDesblock { get; set; }

        [BsonElement("imageUrl")]
        public string ImageUrl { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("mail")]
        public string Mail { get; set; }

        [BsonElement("password")]
        public string Password { get; set; }

        [BsonElement("provider")]
        public string Provider { get; set; }

        [BsonElement("codeDesblock")]
        public string CodeDesblock { get; set; }

        [BsonElement("nuCellPhone")]
        public string NuCellPhone { get; set; }

        [BsonElement("isBlock")]
        public bool IsBlock { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("roles")]
        public ICollection<UserRoleEntity> Roles { get; set; }
    }
}
