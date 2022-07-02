using System.Collections.Generic;
using Newtonsoft.Json;
using Sample.ElectronicCommerce.Core.Entities.DTO;

namespace Sample.ElectronicCommerce.Core.Entities.Settings
{
    public class AppSettings
    {
        [JsonProperty("Version")]
        public string Version { get; set; }

        [JsonProperty("SecretKey")]
        public string SecretKey { get; set; }   
        
        [JsonProperty("IsDebug")]
        public bool IsDebug { get; set; }   

        [JsonProperty("Credentials")]
        public List<UserDTO> Credentials { get; set; }
    }
}