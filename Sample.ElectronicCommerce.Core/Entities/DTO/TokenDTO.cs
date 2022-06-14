using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.DTO
{
    public class TokenDTO
    {
        #region Constructor
        public TokenDTO() { }
        #endregion

        #region Atributtes
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }
        #endregion
    }
}