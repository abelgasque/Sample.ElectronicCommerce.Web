using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;

namespace Sample.ElectronicCommerce.BrokerMail.Entities.EF.Configuration
{
    public class MailBrokerConfig : IEntityTypeConfiguration<MailBrokerEntity>
    {
        public void Configure(EntityTypeBuilder<MailBrokerEntity> builder)
        {
            builder.ToTable(DataBaseConstant.MAIL_BROKER);
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName(DataBaseConstant.ID_MAIL_BROKER);
            builder.Property(e => e.DtCreation).HasColumnName(DataBaseConstant.DT_CREATION);
            builder.Property(e => e.DtLastUpdate).HasColumnName(DataBaseConstant.DT_LAST_UPDATE);
            builder.Property(e => e.Server).HasColumnName(DataBaseConstant.DE_SERVER);
            builder.Property(e => e.Name).HasColumnName(DataBaseConstant.NM_MAIL_BROKER);
            builder.Property(e => e.Password).HasColumnName(DataBaseConstant.NM_PASSWORD);
            builder.Property(e => e.UserName).HasColumnName(DataBaseConstant.DE_USER_NAME);
            builder.Property(e => e.Code).HasColumnName(DataBaseConstant.CODE_MAIL_BROKER);
            builder.Property(e => e.Port).HasColumnName(DataBaseConstant.NU_PORT);
            builder.Property(e => e.IsEnabledSsl).HasColumnName(DataBaseConstant.IS_ENABLED_SSL);
            builder.Property(e => e.IsActive).HasColumnName(DataBaseConstant.IS_ACTIVE);
        }
    }
}
