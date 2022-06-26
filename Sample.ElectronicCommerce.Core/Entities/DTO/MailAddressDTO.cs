namespace Sample.ElectronicCommerce.Core.Entities.DTO
{
    public class MailAddressDTO
    {
        #region Constructor
        public MailAddressDTO() { }
        #endregion

        #region Atributtes                
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }
        public int? Port { get; set; }
        public bool IsEnabledSsl { get; set; }
        #endregion
    }
}