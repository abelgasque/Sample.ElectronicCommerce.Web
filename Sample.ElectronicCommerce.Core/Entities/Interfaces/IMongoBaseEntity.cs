using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Sample.ElectronicCommerce.Core.Entities.Interfaces
{
    public interface IMongoBaseEntity
    {
        public string Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public DateTime? DtCreation { get; set; }

        public DateTime? DtLastUpdate { get; set; }

        public bool IsActive { get; set; }
    }
}
