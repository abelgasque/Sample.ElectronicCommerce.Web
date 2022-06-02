using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.Settings
{
    public class SecuritySettings
    {   
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
