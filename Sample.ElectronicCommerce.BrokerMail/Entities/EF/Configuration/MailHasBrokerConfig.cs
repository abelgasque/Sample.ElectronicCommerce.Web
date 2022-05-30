using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;

namespace Sample.ElectronicCommerce.BrokerMail.Entities.EF.Configuration
{
    public class MailHasBrokerConfig : IEntityTypeConfiguration<MailHasBrokerEntity>
    {
        public void Configure(EntityTypeBuilder<MailHasBrokerEntity> builder)
        {
            builder.ToTable(DataBaseConstant.MAIL_HAS_BROKER);
            
            builder.HasKey(e => new { e.IdMail, e.IdMailBroker });

            //builder.Property(e => e.DtCreation).HasColumnName(DataBaseConstant.DT_CREATION).IsRequired();
            builder.Property(e => e.IdMail).HasColumnName(DataBaseConstant.ID_MAIL).IsRequired();
            builder.Property(e => e.IdMailBroker).HasColumnName(DataBaseConstant.ID_MAIL_BROKER).IsRequired();            
        }    
    }
}
