using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.MongoDb
{
    public class UserEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonElement("code")]
        [JsonProperty("code")]
        public string Code { get; set; } = null;

        [BsonElement("image_url")]
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; } = null;

        [BsonElement("name")]
        [JsonProperty("name")]
        public string Name { get; set; } = null;

        [BsonElement("last_name")]
        [JsonProperty("lastName")]
        public string LastName { get; set; } = null;
        [BsonElement("mail")]
        [JsonProperty("mail")]
        public string Mail { get; set; } = null;

        [BsonElement("password")]
        [JsonProperty("password")]
        public string Password { get; set; } = null;

        [BsonElement("nu_cell_phone")]
        [JsonProperty("nuCellPhone")]
        public string NuCellPhone { get; set; } = null;

        [BsonElement("dt_creation")]
        [JsonProperty("dtCreation")]
        public DateTime? DtCreation { get; set; } = null;

        [BsonElement("dt_last_update")]
        [JsonProperty("dtLastUpdate")]
        public DateTime? DtLastUpdate { get; set; } = null;

        [BsonElement("dt_last_block")]
        [JsonProperty("dtLastBlock")]
        public DateTime? DtLastBlock { get; set; } = null;

        [BsonElement("dt_last_desblock")]
        [JsonProperty("dtLastDesblock")]
        public DateTime? DtLastDesblock { get; set; } = null;

        [BsonElement("nu_auth_attempts_fail")]
        [JsonProperty("nuAuthAttemptsFail")]
        public int NuAuthAttemptsFail { get; set; } = 0;

        [BsonElement("is_block")]
        [JsonProperty("isBlock")]
        public bool IsBlock { get; set; } = false;

        [BsonElement("is_active")]
        [JsonProperty("isActive")]
        public bool IsActive { get; set; } = true;

        [BsonElement("roles")]
        [JsonProperty("roles")]
        public List<string> Roles { get; set; } = null;
    }
}