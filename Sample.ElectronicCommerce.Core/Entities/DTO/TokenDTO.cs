using System;

namespace Sample.ElectronicCommerce.Core.Entities.DTO
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
        public string IdUserSession { get; set; } = null;
        public string AccessToken { get; set; } = null;
        public int ExpiresIn { get; set; } = 0;
        #endregion
    }
}
