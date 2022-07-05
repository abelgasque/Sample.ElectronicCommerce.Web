using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.DTO
{
    public class ResetPasswordDTO
    {
        public ResetPasswordDTO() { }

        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("newPassword")]
        public string NewPassword { get; set; }

        [JsonProperty("unblock")]
        public bool Unblock { get; set; }
    }
}