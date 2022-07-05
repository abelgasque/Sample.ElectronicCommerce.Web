using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.DTO
{
    public class ForgotPasswordDTO
    {
        public ForgotPasswordDTO() { }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("block")]
        public bool Block { get; set; }
    }
}