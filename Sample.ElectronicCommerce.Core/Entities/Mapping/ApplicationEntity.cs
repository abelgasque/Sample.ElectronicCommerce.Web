using System;

namespace Sample.ElectronicCommerce.Core.Entities.EF.Mapping
{
    public class ApplicationEntity
    {
        public long Id { get; set; }

        public DateTime? DtCreation { get; set; }

        public DateTime? DtLastUpdate { get; set; }

        public string DelocalBaseUrl { get; set; }

        public string DeHmlBaseUrl { get; set; }

        public string DeProdBaseUrl { get; set; }

        public string Name { get; set; }

        public string NuVersion { get; set; }

        public bool IsActive { get; set; }
    }
}
