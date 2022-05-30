using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.ElectronicCommerce.Core.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;

namespace Sample.ElectronicCommerce.Core.Entities.EF.Configuration
{
    public class ApplicationConfig : IEntityTypeConfiguration<ApplicationEntity>
    {
        public void Configure(EntityTypeBuilder<ApplicationEntity> builder)
        {
            builder.ToTable(DataBaseConstant.APPLICATION);
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName(DataBaseConstant.ID_APPLICATION);
            builder.Property(e => e.DtCreation).HasColumnName(DataBaseConstant.DT_CREATION).IsRequired();
            builder.Property(e => e.DtLastUpdate).HasColumnName(DataBaseConstant.DT_LAST_UPDATE);
            builder.Property(e => e.DelocalBaseUrl).HasColumnName(DataBaseConstant.DE_LOCAL_BASE_URL).IsRequired();
            builder.Property(e => e.DeHmlBaseUrl).HasColumnName(DataBaseConstant.DE_HML_BASE_URL);
            builder.Property(e => e.DeProdBaseUrl).HasColumnName(DataBaseConstant.DE_PROD_BASE_URL);
            builder.Property(e => e.Name).HasColumnName(DataBaseConstant.NM_APPLICATION).IsRequired();
            builder.Property(e => e.NuVersion).HasColumnName(DataBaseConstant.NU_VERSION).IsRequired();
            builder.Property(e => e.IsActive).HasColumnName(DataBaseConstant.IS_ACTIVE).IsRequired();
        }
    }
}
