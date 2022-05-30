using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Shared.Entities.Settings
{
    public class WebSocketSettings
    {
        [JsonProperty("MongoClient")]
        public MongoClientSettings MongoClient { get; set; }
    }
}
