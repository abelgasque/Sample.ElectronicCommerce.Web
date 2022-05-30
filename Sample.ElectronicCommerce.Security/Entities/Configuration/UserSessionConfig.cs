using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sample.ElectronicCommerce.Security.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;

namespace Sample.ElectronicCommerce.Security.Entities.EF.Configuration
{
    public class UserSessionConfig : IEntityTypeConfiguration<UserSessionEntity>
    {
        public void Configure(EntityTypeBuilder<UserSessionEntity> builder)
        {
            builder.ToTable(DataBaseConstant.USER_SESSION);
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Id).HasColumnName(DataBaseConstant.ID_USER_SESSION);
            builder.Property(e => e.IdUser).HasColumnName(DataBaseConstant.ID_USER);
            builder.Property(e => e.DtCreation).HasColumnName(DataBaseConstant.DT_CREATION);
            builder.Property(e => e.DtLastUpdate).HasColumnName(DataBaseConstant.DT_LAST_UPDATE);
            builder.Property(e => e.DtAccessTokenExpiration).HasColumnName(DataBaseConstant.DT_TOKEN_EXPIRATION);
            builder.Property(e => e.DtRefreshTokenExpiration).HasColumnName(DataBaseConstant.DT_REFRESH_TOKEN_EXPIRATION);
            builder.Property(e => e.AccessToken).HasColumnName(DataBaseConstant.DE_TOKEN);
            builder.Property(e => e.RefreshToken).HasColumnName(DataBaseConstant.DE_REFRESH_TOKEN);
            builder.Property(e => e.DeMessage).HasColumnName(DataBaseConstant.DE_MESSAGE);
            builder.Property(e => e.NuVersion).HasColumnName(DataBaseConstant.NU_VERSION);
            builder.Property(e => e.NuAuthenticationAttempts).HasColumnName(DataBaseConstant.NU_AUTHENTICATION_ATTEMPTS);
            builder.Property(e => e.NuRefreshToken).HasColumnName(DataBaseConstant.NU_REFRESH_TOKEN);
            builder.Property(e => e.IsSuccess).HasColumnName(DataBaseConstant.IS_SUCCESS);
            builder.Property(e => e.IsTest).HasColumnName(DataBaseConstant.IS_TEST);
            builder.Property(e => e.IsActive).HasColumnName(DataBaseConstant.IS_ACTIVE);

            builder.HasOne(e => e.User).WithMany(e => e.Sessions).HasForeignKey(e => e.IdUser);
        }
    }
}
