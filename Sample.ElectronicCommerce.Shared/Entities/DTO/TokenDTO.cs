using System;

namespace Sample.ElectronicCommerce.Shared.Entities.DTO
{
    public class TokenDTO
    {
        #region Constructor
        public TokenDTO() { }

        public TokenDTO(string accessToken, int expiresIn)
        {
            AccessToken = accessToken;
            ExpiresIn = expiresIn;
        }
        #endregion

        #region Atributtes
        public long IdUserSession { get; set; } = 0;
        public string AccessToken { get; set; } = null;
        public int ExpiresIn { get; set; } = 0;
        #endregion
    }
}
