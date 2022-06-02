using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.Settings
{
    public class SharedSettings
    {
        [JsonProperty("DataBase")]
        public DataBaseSettings DataBase { get; set; }
    }
}
