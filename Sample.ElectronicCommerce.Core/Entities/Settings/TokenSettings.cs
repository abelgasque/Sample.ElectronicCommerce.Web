using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.Settings
{
    public class TokenSettings
    {
        [JsonProperty("SecretKey")]
        public string SecretKey { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("ExpireIn")]
        public int ExpireIn { get; set; }

        [JsonProperty("NuAuthAttempts")]
        public int NuAuthAttempts { get; set; }        
    }
}