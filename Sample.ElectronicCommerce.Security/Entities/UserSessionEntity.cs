using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Sample.ElectronicCommerce.Core.Entities.Base;

namespace Sample.ElectronicCommerce.Security.Entities
{
    public class UserSessionEntity : MongoBaseEntity
    {
        public UserSessionEntity() { }

        [BsonElement("id_user")]
        [JsonProperty("idUser")]
        public string IdUser { get; set; }
        
        [BsonElement("dt_last_block")]
        [JsonProperty("dtLastBlock")]
        public DateTime? DtLastBlock { get; set; } = null;

        [BsonElement("dt_last_desblock")]
        [JsonProperty("dtLastDesblock")]
        public DateTime? DtLastDesblock { get; set; } = null;

        [BsonElement("access_token")]
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; } = null;

        [BsonElement("version")]
        [JsonProperty("version")]
        public string Version { get; set; } = null;

        [BsonElement("password")]
        [JsonProperty("password")]
        public string Password { get; set; } = null;

        [BsonElement("nu_refresh_token")]
        [JsonProperty("nuRefreshToken")]
        public int NuRefreshToken { get; set; } = 0;

        [BsonElement("nu_auth_attempts_token")]
        [JsonProperty("nuAuthAttemptsToken")]
        public int NuAuthAttemptsToken { get; set; } = 1;

        [BsonElement("nu_success_token")]
        [JsonProperty("nuSuccessToken")]
        public int NuSuccessToken { get; set; } = 0;

        [BsonElement("nu_fails_token")]
        [JsonProperty("nuFailsToken")]
        public int NuFailsToken { get; set; } = 0;

        [BsonElement("is_test")]
        [JsonProperty("isTest")]
        public bool IsTest { get; set; } = false;

        [BsonElement("is_loggout")]
        [JsonProperty("isLoggout")]
        public bool IsLoggout { get; set; } = false;

        [BsonElement("user")]
        [JsonProperty("user")]
        public UserEntity User { get; set; } = null;

        [BsonElement("roles")]
        [JsonProperty("roles")]
        public ICollection<RoleEntity> Roles { get; set; }
    }
} 