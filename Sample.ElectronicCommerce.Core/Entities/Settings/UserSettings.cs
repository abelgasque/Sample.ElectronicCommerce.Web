using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.Settings
{
    public class UserSettings
    {
        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Mail")]
        public string Mail { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }
    }
}