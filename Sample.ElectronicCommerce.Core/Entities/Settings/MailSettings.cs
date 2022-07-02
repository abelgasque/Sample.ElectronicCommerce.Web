using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.Settings
{
    public class MailSettings
    {
        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("Mail")]
        public string Mail { get; set; }
         
        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("Server")]
        public string Server { get; set; }
        
        [JsonProperty("Port")]
        public int? Port { get; set; }
        
        [JsonProperty("IsEnabledSsl")]
        public bool IsEnabledSsl { get; set; }
    }
}