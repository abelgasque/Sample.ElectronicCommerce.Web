using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Shared.Entities.Settings
{
    public class SecuritySettings
    {   
        [JsonProperty("Secret")]
        public string Secret { get; set; }

        [JsonProperty("ExpireIn")]
        public int ExpireIn { get; set; }

        [JsonProperty("Type")]
        public string Type { get; set; }

        [JsonProperty("NuAuthenticationAttempts")]
        public int NuAuthenticationAttempts { get; set; }

        [JsonProperty("MongoClient")]
        public MongoClientSettings MongoClient { get; set; }        

        [JsonProperty("UserColletion")]
        public string UserColletion { get; set; }

        [JsonProperty("UserRoleColletion")]
        public string UserRoleColletion { get; set; }

        [JsonProperty("UserSessionColletion")]
        public string UserSessionColletion { get; set; }

    }
}
