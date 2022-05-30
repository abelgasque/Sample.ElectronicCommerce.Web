using System;

namespace Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping
{
    public class MailEntity
    {
        public long Id { get; set; }

		public DateTime DtCreation { get; set; }

		public DateTime? DtLastUpdate { get; set; }

		public string Body { get; set; }

		public string Title { get; set; }

		public string Name { get; set; }

		public string Code { get; set; }

		public decimal VlMailUnit { get; set; }

		public decimal VlMailMass { get; set; }

		public bool IsPriority { get; set; }

		public bool IsActive { get; set; }

		//public ICollection<MailHasBrokerEntity> Brokers { get; set; }
	}
}
