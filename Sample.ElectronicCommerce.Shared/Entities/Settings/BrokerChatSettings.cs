using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Shared.Entities.Settings
{
    public class BrokerChatSettings
    {
        [JsonProperty("MongoClient")]
        public MongoClientSettings MongoClient { get; set; }

        [JsonProperty("BrokerChatColletion")]
        public string BrokerChatColletion { get; set; }
    }
}