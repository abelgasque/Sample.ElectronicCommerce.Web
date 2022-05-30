using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Sample.ElectronicCommerce.Security.Entities.EF.Mapping
{
    public class UserRoleEntity
    {
        public long Id { get; set; }
        
        public string Code { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        [JsonIgnore]
        public List<UserEntity> Users { get; set; }
        
        [JsonIgnore]
        public List<UserHasRoleEntity> UserHasRole { get; set; }
    }
}
