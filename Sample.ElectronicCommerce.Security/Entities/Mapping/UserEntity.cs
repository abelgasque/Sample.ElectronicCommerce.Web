using Sample.ElectronicCommerce.Shared.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sample.ElectronicCommerce.Security.Entities.EF.Mapping
{
    public class UserEntity : AppBaseEntity
    {
        public long Id { get; set; }

        public DateTime? DtCreation { get; set; }

        public DateTime? DtLastUpdate { get; set; }
        
        public DateTime? DtLastBlock { get; set; }
        
        public DateTime? DtLastDesblock { get; set; }
        
        public string ImageUrl { get; set; }

        public string Name { get; set; }
        
        public string LastName { get; set; }

        public string Mail { get; set; }        

        public string Password { get; set; }

        public string Provider { get; set; }

        public string CodeDesblock { get; set; }
                
        public string NuCellPhone { get; set; }
        
        public bool IsBlock { get; set; }

        public bool IsActive { get; set; }
        
        [Newtonsoft.Json.JsonIgnore]
        public List<UserRoleEntity> Roles { get; set; }
        
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public List<UserHasRoleEntity> UserHasRole { get; set; }

        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public List<UserSessionEntity> Sessions { get; set; }
    }
}
