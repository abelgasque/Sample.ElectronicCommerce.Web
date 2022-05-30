using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.ElectronicCommerce.Security.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;

namespace Sample.ElectronicCommerce.Security.Entities.EF.Configuration
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRoleEntity>
    {
        public void Configure(EntityTypeBuilder<UserRoleEntity> builder)
        {
            builder.ToTable(DataBaseConstant.USER_ROLE);
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Id).HasColumnName(DataBaseConstant.ID_USER_ROLE);
            builder.Property(e => e.Code).HasColumnName(DataBaseConstant.CODE_ROLE).IsRequired();
            builder.Property(e => e.Name).HasColumnName(DataBaseConstant.NM_ROLE).IsRequired();
            builder.Property(e => e.IsActive).HasColumnName(DataBaseConstant.IS_ACTIVE).IsRequired();
        }
    }
}
