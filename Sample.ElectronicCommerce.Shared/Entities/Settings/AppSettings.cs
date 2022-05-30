using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Shared.Entities.Settings
{
    public class AppSettings
    {
        [JsonProperty("Version")]
        public string Version { get; set; }

        [JsonProperty("IsTest")]
        public bool IsTest { get; set; }
    }
}
