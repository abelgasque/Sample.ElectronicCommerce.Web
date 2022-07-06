using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.MongoDB
{
    public class UserEntity
    {
        public UserEntity() { }

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

        [BsonElement("user_name")]
        [JsonProperty("username")]
        public string UserName { get; set; } = null;

        [BsonElement("password")]
        [JsonProperty("password")]
        public string Password { get; set; } = null;

        [BsonElement("phone")]
        [JsonProperty("phone")]
        public string Phone { get; set; } = null;

        [BsonElement("status")]
        [JsonProperty("status")]
        public string Status { get; set; } = null;

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

        [BsonElement("nu_auth_attempts")]
        [JsonProperty("nuAuthAttempts")]
        public int NuAuthAttempts { get; set; } = 0;

        [BsonElement("roles")]
        [JsonProperty("roles")]
        public List<string> Roles { get; set; } = null;
    }
}