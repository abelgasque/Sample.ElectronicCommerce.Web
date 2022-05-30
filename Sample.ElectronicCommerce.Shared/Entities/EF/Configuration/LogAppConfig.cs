using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.Mapping;

namespace Sample.ElectronicCommerce.Shared.Entities.Configuration
{
    public class LogAppConfig : IEntityTypeConfiguration<LogAppEntity>
    {
        public void Configure(EntityTypeBuilder<LogAppEntity> builder)
        {
            builder.ToTable(DataBaseConstant.LOG_APP);
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName(DataBaseConstant.ID_LOG_APP);
            builder.Property(e => e.IdApplication).HasColumnName(DataBaseConstant.ID_APPLICATION);
            builder.Property(e => e.IdUserSession).HasColumnName(DataBaseConstant.ID_USER_SESSION);
            builder.Property(e => e.DtCreation).HasColumnName(DataBaseConstant.DT_CREATION);
            builder.Property(e => e.DtLastUpdate).HasColumnName(DataBaseConstant.DT_LAST_UPDATE);
            builder.Property(e => e.NuVersion).HasColumnName(DataBaseConstant.NU_VERSION);
            builder.Property(e => e.NmMethod).HasColumnName(DataBaseConstant.NM_METHOD);
            builder.Property(e => e.DeContent).HasColumnName(DataBaseConstant.DE_CONTENT);
            builder.Property(e => e.DeResult).HasColumnName(DataBaseConstant.DE_RESULT);
            builder.Property(e => e.DeMessage).HasColumnName(DataBaseConstant.DE_MESSAGE);
            builder.Property(e => e.DeExceptionMessage).HasColumnName(DataBaseConstant.DE_EXCEPTION_MESSAGE);
            builder.Property(e => e.DeStackTrace).HasColumnName(DataBaseConstant.DE_STACK_TRACE);
            builder.Property(e => e.IsSuccess).HasColumnName(DataBaseConstant.IS_SUCCESS);
            builder.Property(e => e.IsTest).HasColumnName(DataBaseConstant.IS_TEST);
            builder.Property(e => e.IsActive).HasColumnName(DataBaseConstant.IS_ACTIVE);
        }
    }
}
