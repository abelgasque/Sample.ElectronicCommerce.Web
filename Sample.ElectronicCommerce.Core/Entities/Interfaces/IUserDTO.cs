using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Entities.Interfaces
{
    public interface IUserDTO
    {
        [JsonProperty("mail")]
        string Mail { get; set; }

        [JsonProperty("password")]
        string Password { get; set; }

    }
}
