using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Shared.Entities.Settings
{
    public class CoreSettings
    {
        [JsonProperty("MongoClient")]
        public MongoClientSettings MongoClient { get; set; }

        [JsonProperty("ApplicationColletion")]
        public string ApplicationColletion { get; set; }
    }
}
