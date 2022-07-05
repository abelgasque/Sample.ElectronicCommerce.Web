using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.DTO
{
    public class UserDTO
    {
        public UserDTO() { }

        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
