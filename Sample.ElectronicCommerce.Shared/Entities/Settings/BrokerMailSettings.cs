using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Shared.Entities.Settings
{
    public class BrokerMailSettings
    {
        [JsonProperty("MongoClient")]
        public MongoClientSettings MongoClient { get; set; }

        [JsonProperty("BrokerMailColletion")]
        public string BrokerMailColletion { get; set; }

        [JsonProperty("MailColletion")]
        public string MailColletion { get; set; }

        [JsonProperty("MailMessageColletion")]
        public string MailMessageColletion { get; set; }
    }
}
