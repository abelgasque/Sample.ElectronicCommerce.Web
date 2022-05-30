using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Shared.Entities.Settings
{
    public class SharedSettings
    {
        [JsonProperty("DataBase")]
        public DataBaseSettings DataBase { get; set; }
    }
}
