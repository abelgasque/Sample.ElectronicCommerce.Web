using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Shared.Entities.Settings
{
    public class SecuritySettings
    {
        [JsonProperty("DataBase")]
        public DataBaseSettings DataBase { get; set; }
    }
}
