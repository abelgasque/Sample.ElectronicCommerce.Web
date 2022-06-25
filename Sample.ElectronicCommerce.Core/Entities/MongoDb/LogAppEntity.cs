using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.DataBase.Mapping
{
    public class LogAppEntity
    {
        public LogAppEntity() { }
		
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id")]
        [JsonProperty("id")]
        public string Id { get; set; }

        [BsonElement("id_organization")]
        [JsonProperty("idOrganization")]
        public string IdOrganization { get; set; }

        [BsonElement("id_user")]
        [JsonProperty("idUser")]
        public string IdUser { get; set; }

        [BsonElement("dt_creation")]
        [JsonProperty("dtCreation")]
        public DateTime? DtCreation { get; set; }

        [BsonElement("dt_last_update")]
        [JsonProperty("dtLastUpdate")]
        public DateTime? DtLastUpdate { get; set; }

        [BsonElement("version")]
        [JsonProperty("version")]
        public string Version { get; set; }

        [BsonElement("method")]
        [JsonProperty("method")]
        public string Method { get; set; }

        [BsonElement("content")]
        [JsonProperty("content")]
        public string Content { get; set; }

        [BsonElement("result")]
        [JsonProperty("result")]
        public string Result { get; set; }

        [BsonElement("message")]
        [JsonProperty("message")]
        public string Message { get; set; }

        [BsonElement("exception_message")]
        [JsonProperty("exceptionMessage")]
        public string ExceptionMessage { get; set; }

        [BsonElement("stack_trace")]
        [JsonProperty("stackTrace")]
        public string StackTrace { get; set; }

        [BsonElement("is_success")]
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [BsonElement("is_debug")]
        [JsonProperty("isDebug")]
        public bool IsDebug { get; set; }

        [BsonElement("is_active")]
        [JsonProperty("isActive")]
        public bool IsActive { get; set; }
    }
}
