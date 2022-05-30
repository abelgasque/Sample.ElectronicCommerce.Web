using System;

namespace Sample.ElectronicCommerce.Security.Entities.EF.Mapping
{    
    public class UserHasRoleEntity
    {
        public DateTime DtCreation { get; set; } = DateTime.Now;

        public long IdUserRole { get; set; }
        public UserRoleEntity UserRole { get; set; }

        public long IdUser { get; set; }
        public UserEntity User { get; set; }
    }
}
