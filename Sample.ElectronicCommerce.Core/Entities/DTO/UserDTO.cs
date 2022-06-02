using Sample.ElectronicCommerce.Core.Entities.Interfaces;

namespace Sample.ElectronicCommerce.Core.Entities.DTO
{
    public class UserDTO : IUserDTO
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
