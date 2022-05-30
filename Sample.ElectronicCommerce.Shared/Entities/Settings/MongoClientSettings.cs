using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Shared.Entities.Settings
{
    public class MongoClientSettings
    {
        [JsonProperty("ConnectionString")]
        public string ConnectionString { get; set; }
        
        [JsonProperty("Server")]
        public string Server { get; set; }

        [JsonProperty("DataBase")]
        public string DataBase { get; set; }
        
        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("DocMessage")]
        public string DocMessage { get; set; }

        public string GetConnectionString
        {
            get
            {
                return string.Format(ConnectionString, UserName, Password, Server);
            }
        }
    }
}
