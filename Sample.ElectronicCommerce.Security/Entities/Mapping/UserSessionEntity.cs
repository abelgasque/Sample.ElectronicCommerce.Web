using System;

namespace Sample.ElectronicCommerce.Security.Entities.EF.Mapping
{
    public class UserSessionEntity
    {
        public long Id { get; set; }

        public long IdUser { get; set; }

        public DateTime DtCreation { get; set; }

        public DateTime? DtLastUpdate { get; set; }

        public DateTime? DtAccessTokenExpiration { get; set; }

        public DateTime? DtRefreshTokenExpiration { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string DeMessage { get; set; }

        public string NuVersion { get; set; }

        public int NuAuthenticationAttempts { get; set; }

        public int? NuRefreshToken { get; set; }

        public bool IsSuccess { get; set; }

        public bool IsTest { get; set; }

        public bool IsActive { get; set; }

        public virtual UserEntity User { get; set; }
    }
} 