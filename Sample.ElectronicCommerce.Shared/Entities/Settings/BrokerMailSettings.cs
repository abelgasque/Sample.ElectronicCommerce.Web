using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Shared.Entities.Settings
{
    public class BrokerMailSettings
    {
        [JsonProperty("DataBase")]
        public DataBaseSettings DataBase { get; set; }
    }
}
