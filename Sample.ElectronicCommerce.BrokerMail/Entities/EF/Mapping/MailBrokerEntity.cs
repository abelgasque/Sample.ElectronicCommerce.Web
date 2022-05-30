using System;

namespace Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping
{
    public class MailBrokerEntity
    {        
        public long Id { get; set; }

		public DateTime? DtCreation { get; set; }

		public DateTime? DtLastUpdate { get; set; }		

		public string Server { get; set; }

		public string Name { get; set; }

		public string Password { get; set; }

		public string UserName { get; set; }

		public string Code { get; set; }

		public int? Port { get; set; }

		public bool IsEnabledSsl { get; set; }

		public bool IsActive { get; set; }
	}
}
