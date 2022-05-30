using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;

namespace Sample.ElectronicCommerce.BrokerMail.Entities.EF.Configuration
{
    public class MailConfig : IEntityTypeConfiguration<MailEntity>
    {
        public void Configure(EntityTypeBuilder<MailEntity> builder)
        {
            builder.ToTable(DataBaseConstant.MAIL);
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName(DataBaseConstant.ID_MAIL);
            builder.Property(e => e.DtCreation).HasColumnName(DataBaseConstant.DT_CREATION);
            builder.Property(e => e.DtLastUpdate).HasColumnName(DataBaseConstant.DT_LAST_UPDATE);
			builder.Property(e => e.Body).HasColumnName(DataBaseConstant.DE_BODY);
            builder.Property(e => e.Title).HasColumnName(DataBaseConstant.DE_TITLE);
            builder.Property(e => e.Name).HasColumnName(DataBaseConstant.NM_MAIL);
            builder.Property(e => e.Code).HasColumnName(DataBaseConstant.CODE_MAIL);
            builder.Property(e => e.VlMailUnit).HasColumnName(DataBaseConstant.VL_MAIL_UNIT);
            builder.Property(e => e.VlMailMass).HasColumnName(DataBaseConstant.VL_MAIL_UNIT);
            builder.Property(e => e.IsPriority).HasColumnName(DataBaseConstant.IS_PRIORITY);
            builder.Property(e => e.IsActive).HasColumnName(DataBaseConstant.IS_ACTIVE);
        }
    }
}
