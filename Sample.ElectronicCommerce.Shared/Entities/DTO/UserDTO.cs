namespace Sample.ElectronicCommerce.Shared.Entities.DTO
{
    public class UserDTO
    {
        #region Constructor
        public UserDTO() { }

        public UserDTO(string mail, string password)
        {
            Mail = mail;
            Password = password;
        }
        #endregion

        #region Atributtes
        public string Mail { get; set; }

        public string Password { get; set; }
        #endregion
    }
}
