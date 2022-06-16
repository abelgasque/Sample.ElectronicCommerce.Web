using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.Settings
{
    public class AppSettings
    {
        [JsonProperty("Key")]
        public string Key { get; set; }

        [JsonProperty("BaseUrl")]
        public string BaseUrl { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Code")]
        public string Code { get; set; }

        [JsonProperty("ImageLogoPath")]
        public string ImageLogoPath { get; set; }

        [JsonProperty("Color")]
        public string Color { get; set; }

        [JsonProperty("BackgronudColor")]
        public string BackgronudColor { get; set; }

        [JsonProperty("Version")]
        public string Version { get; set; }

        [JsonProperty("IsDebug")]
        public bool IsDebug { get; set; }
    }
}
