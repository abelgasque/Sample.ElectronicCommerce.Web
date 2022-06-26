namespace Sample.ElectronicCommerce.Core.Entities.DTO
{
    public class MailMessageDTO
    {
        #region Constructor
        public MailMessageDTO() { }
        #endregion

        #region Atributtes
        public string Mail { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public bool IsPriority { get; set; }
        #endregion
    }
}