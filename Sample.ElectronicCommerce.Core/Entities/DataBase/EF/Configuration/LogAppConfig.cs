using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.ElectronicCommerce.Core.Entities.DataBase.Mapping;
using Sample.ElectronicCommerce.Core.Util;

namespace Sample.ElectronicCommerce.Core.Entities.DataBase.Configuration
{
    public class LogAppConfig : IEntityTypeConfiguration<LogAppEntity>
    {
        public void Configure(EntityTypeBuilder<LogAppEntity> builder)
        {
            builder.ToTable(AppConstant.LOG_APP);
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName(AppConstant.ID_LOG_APP);
            builder.Property(e => e.IdApplication).HasColumnName(AppConstant.ID_APPLICATION);
            builder.Property(e => e.IdUserSession).HasColumnName(AppConstant.ID_USER_SESSION);
            builder.Property(e => e.DtCreation).HasColumnName(AppConstant.DT_CREATION);
            builder.Property(e => e.DtLastUpdate).HasColumnName(AppConstant.DT_LAST_UPDATE);
            builder.Property(e => e.NuVersion).HasColumnName(AppConstant.NU_VERSION);
            builder.Property(e => e.NmMethod).HasColumnName(AppConstant.NM_METHOD);
            builder.Property(e => e.DeContent).HasColumnName(AppConstant.DE_CONTENT);
            builder.Property(e => e.DeResult).HasColumnName(AppConstant.DE_RESULT);
            builder.Property(e => e.DeMessage).HasColumnName(AppConstant.DE_MESSAGE);
            builder.Property(e => e.DeExceptionMessage).HasColumnName(AppConstant.DE_EXCEPTION_MESSAGE);
            builder.Property(e => e.DeStackTrace).HasColumnName(AppConstant.DE_STACK_TRACE);
            builder.Property(e => e.IsSuccess).HasColumnName(AppConstant.IS_SUCCESS);
            builder.Property(e => e.IsTest).HasColumnName(AppConstant.IS_TEST);
            builder.Property(e => e.IsActive).HasColumnName(AppConstant.IS_ACTIVE);
        }
    }
}
