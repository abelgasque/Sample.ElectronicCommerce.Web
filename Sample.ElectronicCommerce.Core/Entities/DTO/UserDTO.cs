using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.DTO
{
    public class UserDTO
    {
        public UserDTO() { }

        [JsonProperty("userName")]
        public string UserName { get; set; }


        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
