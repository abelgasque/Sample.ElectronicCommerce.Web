namespace Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping
{
    public class MailHasBrokerEntity
    {
        //public DateTime DtCreation { get; set; }

        public long IdMail { get; set; }

        public MailEntity Mail { get; set; }

        public long IdMailBroker { get; set; }

        public MailBrokerEntity MailBroker { get; set; }
    }
}
