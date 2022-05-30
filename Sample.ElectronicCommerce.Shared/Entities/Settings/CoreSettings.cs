using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Shared.Entities.Settings
{
    public class CoreSettings
    {
        [JsonProperty("DataBase")]
        public DataBaseSettings DataBase { get; set; }
    }
}
