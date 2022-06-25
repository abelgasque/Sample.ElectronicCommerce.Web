using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.Settings
{
    public class MongoClientSettings
    {
        [JsonProperty("ConnectionString")]
        public string ConnectionString { get; set; }

        [JsonProperty("Server")]
        public string Server { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("DataBaseProduction")]
        public string DataBaseProduction { get; set; }

        [JsonProperty("DataBaseTestUnit")]
        public string DataBaseTestUnit { get; set; }

        [JsonProperty("DataBaseTestMass")]
        public string DataBaseTestMass { get; set; }

        [JsonProperty("MailBrokerColletion")]
        public string MailBrokerColletion { get; set; }

        [JsonProperty("MailGroupColletion")]
        public string MailGroupColletion { get; set; }

        [JsonProperty("MailSingleColletion")]
        public string MailSingleColletion { get; set; }

        [JsonProperty("OrganizationColletion")]
        public string OrganizationColletion { get; set; }

        [JsonProperty("ChatBrokerAllColletion")]
        public string ChatBrokerAllColletion { get; set; }

        [JsonProperty("UserColletion")]
        public string UserColletion { get; set; }

        [JsonProperty("UserRoleColletion")]
        public string UserRoleColletion { get; set; }

        [JsonProperty("LogAppColletion")]
        public string LogAppColletion { get; set; }

        public string GetConnectionString
        {
            get
            {
                return string.Format(ConnectionString, UserName, Password, Server);
            }
        }
    }
}
