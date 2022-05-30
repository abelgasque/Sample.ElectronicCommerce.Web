using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;

namespace Sample.ElectronicCommerce.BrokerMail.Entities.EF.Configuration
{
    public class MailMessageConfig : IEntityTypeConfiguration<MailMessageEntity>
    {
        public void Configure(EntityTypeBuilder<MailMessageEntity> builder)
        {
            builder.ToTable(DataBaseConstant.MAIL_MESSAGE);
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName(DataBaseConstant.ID_MAIL_MESSAGE);
            builder.Property(e => e.IdMail).HasColumnName(DataBaseConstant.ID_MAIL);
            builder.Property(e => e.IdMailBroker).HasColumnName(DataBaseConstant.ID_MAIL_BROKER);
            builder.Property(e => e.IdUser).HasColumnName(DataBaseConstant.ID_USER);
            builder.Property(e => e.DtCreation).HasColumnName(DataBaseConstant.DT_CREATION);
            builder.Property(e => e.DtLastUpdate).HasColumnName(DataBaseConstant.DT_LAST_UPDATE);
            builder.Property(e => e.DtSentBroker).HasColumnName(DataBaseConstant.DT_SENT_BROKER);
            builder.Property(e => e.Body).HasColumnName(DataBaseConstant.DE_BODY);
            builder.Property(e => e.Mail).HasColumnName(DataBaseConstant.DE_MAIL);
            builder.Property(e => e.Title).HasColumnName(DataBaseConstant.DE_TITLE);
            builder.Property(e => e.NuVersion).HasColumnName(DataBaseConstant.NU_VERSION);
            builder.Property(e => e.WasSentBroker).HasColumnName(DataBaseConstant.WAS_SENT_BROKER);
            builder.Property(e => e.IsFree).HasColumnName(DataBaseConstant.IS_FREE);
            builder.Property(e => e.IsPriority).HasColumnName(DataBaseConstant.IS_PRIORITY);
            builder.Property(e => e.IsSuccess).HasColumnName(DataBaseConstant.IS_SUCCESS);
            builder.Property(e => e.IsTest).HasColumnName(DataBaseConstant.IS_TEST);
            builder.Property(e => e.IsActive).HasColumnName(DataBaseConstant.IS_ACTIVE);
        }
    }
}
