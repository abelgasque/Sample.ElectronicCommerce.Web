using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.ElectronicCommerce.Security.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;

namespace Sample.ElectronicCommerce.Security.Entities.EF.Configuration
{
    public class UserHasRoleConfig : IEntityTypeConfiguration<UserHasRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserHasRoleEntity> builder)
        {
            builder.ToTable(DataBaseConstant.USER_HAS_ROLE);
            builder.HasKey(e => e.IdUserRole);
            builder.HasKey(e => e.IdUser);
            builder.Property(e => e.IdUser).HasColumnName(DataBaseConstant.ID_USER).IsRequired();
            builder.Property(e => e.IdUserRole).HasColumnName(DataBaseConstant.ID_USER_ROLE).IsRequired();
            builder.Property(e => e.DtCreation).HasColumnName(DataBaseConstant.DT_CREATION).IsRequired();
        }
    }
}
