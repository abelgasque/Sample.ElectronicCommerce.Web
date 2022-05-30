using System;

namespace Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping
{
    public class MailMessageEntity
    {
        public long Id { get; set; }

        public long IdMail { get; set; }
        
        public long IdMailBroker { get; set; }
                
        public long IdUser { get; set; }

        public DateTime? DtCreation { get; set; }

        public DateTime? DtLastUpdate { get; set; }        

        public DateTime? DtSentBroker { get; set; }
        
        public string Body { get; set; }
        
        public string Mail { get; set; }
                
        public string Title { get; set; }
        
        public string NuVersion { get; set; }

        public bool WasSentBroker { get; set; }

        public bool IsFree { get; set; }

        public bool IsPriority { get; set; }

        public bool IsSuccess { get; set; }

        public bool IsTest { get; set; }

        public bool IsActive { get; set; }
    }
}
