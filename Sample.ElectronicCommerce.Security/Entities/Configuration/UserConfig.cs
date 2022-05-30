using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.ElectronicCommerce.Security.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;

namespace Sample.ElectronicCommerce.Security.Entities.EF.Configuration
{
    public class UserConfig : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.ToTable(DataBaseConstant.USER);
            builder.HasKey(e => e.Id);            
            
            builder.Property(e => e.Id).HasColumnName(DataBaseConstant.ID_USER);
            builder.Property(e => e.DtCreation).HasColumnName(DataBaseConstant.DT_CREATION).IsRequired();
            builder.Property(e => e.DtLastUpdate).HasColumnName(DataBaseConstant.DT_LAST_UPDATE);
            builder.Property(e => e.DtLastBlock).HasColumnName(DataBaseConstant.DT_LAST_BLOCK);
            builder.Property(e => e.DtLastDesblock).HasColumnName(DataBaseConstant.DT_LAST_DESBLOCK);
            builder.Property(e => e.ImageUrl).HasColumnName(DataBaseConstant.DE_USER_IMG_URL);
            builder.Property(e => e.Mail).HasColumnName(DataBaseConstant.DE_MAIL).IsRequired();
            builder.Property(e => e.Name).HasColumnName(DataBaseConstant.NM_USER).IsRequired();
            builder.Property(e => e.LastName).HasColumnName(DataBaseConstant.NM_USER_LAST_NAME).IsRequired();
            builder.Property(e => e.Password).HasColumnName(DataBaseConstant.NM_PASSWORD).IsRequired();
            builder.Property(e => e.Provider).HasColumnName(DataBaseConstant.NM_PROVIDER).IsRequired();
            builder.Property(e => e.CodeDesblock).HasColumnName(DataBaseConstant.CODE_DESBLOCK);
            builder.Property(e => e.NuCellPhone).HasColumnName(DataBaseConstant.NU_CELL_PHONE).IsRequired();
            builder.Property(e => e.IsBlock).HasColumnName(DataBaseConstant.IS_BLOCK).IsRequired();
            builder.Property(e => e.IsActive).HasColumnName(DataBaseConstant.IS_ACTIVE).IsRequired();

            builder.HasIndex(e => e.Mail).IsUnique();
            builder.Ignore(e => e.IdUserSession);

            builder.HasMany(pt => pt.Roles)
                .WithMany(pt => pt.Users)
                .UsingEntity<UserHasRoleEntity>(
                    at => at
                        .HasOne(pt => pt.UserRole)
                        .WithMany(pt => pt.UserHasRole)
                        .HasForeignKey(pt => pt.IdUserRole),
                    at => at
                        .HasOne(pt => pt.User)
                        .WithMany(pt => pt.UserHasRole)
                        .HasForeignKey(pt => pt.IdUser),
                    at =>
                    {
                        at.Property(pt => pt.DtCreation).HasDefaultValueSql(DataBaseConstant.DT_CREATION);
                        at.HasKey(pt => new { pt.IdUserRole, pt.IdUser });
                    }
                );

            builder.HasMany(e => e.Sessions).WithOne(e => e.User).HasForeignKey(e => e.Id);
        } 
    }
}
