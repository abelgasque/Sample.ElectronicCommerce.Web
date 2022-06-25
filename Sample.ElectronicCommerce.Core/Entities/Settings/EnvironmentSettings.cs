using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.Settings
{
    public class EnvironmentSettings
    {
        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Mail")]
        public string Mail { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }
        
        [JsonProperty("Secret")]
        public string Secret { get; set; }

        [JsonProperty("ExpireIn")]
        public int ExpireIn { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("NuAuthenticationAttempts")]
        public int NuAuthenticationAttempts { get; set; }
    }
}