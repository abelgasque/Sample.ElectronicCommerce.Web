using System.Collections.Generic;
using Newtonsoft.Json;

namespace Sample.ElectronicCommerce.Core.Entities.Settings
{
    public abstract class UserSettings
    {
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("ImageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }

        [JsonProperty("LastName")]
        public string LastName { get; set; }

        [JsonProperty("UserName")]
        public string UserName { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("Phone")]
        public string Phone { get; set; }

        [JsonProperty("MailServer")]
        public string MailServer { get; set; }

        [JsonProperty("MailPort")]
        public int? MailPort { get; set; }

        [JsonProperty("MailEnabledSsl")]
        public bool MailEnabledSsl { get; set; }

        [JsonProperty("Roles")]
        public List<string> Roles { get; set; }
    }
}