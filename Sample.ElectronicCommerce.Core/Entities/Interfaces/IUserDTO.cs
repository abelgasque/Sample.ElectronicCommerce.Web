using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.Interfaces
{
    public interface IUserDTO
    {
        [JsonProperty("mail")]
        string Mail { get; set; }

        [JsonProperty("password")]
        string Password { get; set; }
    }
}