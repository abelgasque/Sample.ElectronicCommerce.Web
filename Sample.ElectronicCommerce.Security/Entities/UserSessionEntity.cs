using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Sample.ElectronicCommerce.Security.Entities
{
    public class UserSessionEntity
    {
        public UserSessionEntity() { }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        public string Id { get; set; }

        [BsonElement("idUser")]
        public string IdUser { get; set; }

        [BsonElement("dtCreation")]
        public DateTime DtCreation { get; set; }

        [BsonElement("dtLastUpdate")]
        public DateTime? DtLastUpdate { get; set; }

        [BsonElement("dtAccessTokenExpiration")]
        public DateTime? DtAccessTokenExpiration { get; set; }

        [BsonElement("dtRefreshTokenExpiration")]
        public DateTime? DtRefreshTokenExpiration { get; set; }

        [BsonElement("accessToken")]
        public string AccessToken { get; set; }

        [BsonElement("refreshToken")]
        public string RefreshToken { get; set; }

        [BsonElement("message")]
        public string Message { get; set; }

        [BsonElement("version")]
        public string Version { get; set; }

        [BsonElement("nuAuthenticationAttempts")]
        public int NuAuthenticationAttempts { get; set; }

        [BsonElement("nuRefreshToken")]
        public int? NuRefreshToken { get; set; }

        [BsonElement("isSuccess")]
        public bool IsSuccess { get; set; }

        [BsonElement("isTest")]
        public bool IsTest { get; set; }

        [BsonElement("isFinalized")]
        public bool IsFinalized { get; set; }

        [BsonElement("isActive")]
        public bool IsActive { get; set; }

        [BsonElement("user")]
        public UserEntity User { get; set; }
    }
} 