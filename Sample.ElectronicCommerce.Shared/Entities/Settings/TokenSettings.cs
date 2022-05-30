using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Shared.Entities.Settings
{
    public class TokenSettings
    {
        [JsonProperty("Secret")]
        public string Secret { get; set; }

        [JsonProperty("ExpireIn")]
        public int ExpireIn { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }
    }
}
