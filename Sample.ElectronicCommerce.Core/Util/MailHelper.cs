using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace Sample.ElectronicCommerce.Core.Util
{
    public class MailHelper
    {

        private readonly UserSettings _userSettings;

        public MailHelper(
            IOptions<UserSettings> userSettings
        )
        {
            _userSettings = userSettings.Value;
        }

        #region Methods Sending Commom
        private void SendMailWithGenericAddress(MailAddressDTO pAddress, MailMessageDTO pMessage)
        {
            MailAddress toAddress = new MailAddress(pMessage.Mail);
            MailAddress fromAddress = new MailAddress(pAddress.UserName);

            var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = pMessage.Title,
                Body = pMessage.Body,
                IsBodyHtml = true,
                HeadersEncoding = Encoding.UTF8,
                SubjectEncoding = Encoding.UTF8,
                BodyEncoding = Encoding.UTF8,
            };
            if (pMessage.IsPriority) message.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient(pAddress.Server);
            if (pAddress.Port != null) client.Port = pAddress.Port.Value;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = pAddress.IsEnabledSsl;
            client.UseDefaultCredentials = false;
            NetworkCredential smtpUserInfo = new NetworkCredential(pAddress.UserName, pAddress.Password);
            client.Credentials = smtpUserInfo;

            client.SendMailAsync(message);
            client.Dispose();
            message.Dispose();
        }

        private void SendMailWithAddressApp(MailMessageDTO pMessage)
        {
            MailAddressDTO address = new MailAddressDTO()
            {
                UserName = _userSettings.UserName,
                Password = _userSettings.Password,
                Server = _userSettings.MailServer,
                Port = _userSettings.MailPort,
                IsEnabledSsl = _userSettings.MailEnabledSsl,
            };
            this.SendMailWithGenericAddress(address, pMessage);
        }
        #endregion


        #region Methods Sending Public
        public void SendMailBasicWithApplicationAddress(string pMail, string pMessage)
        {
            MailMessageDTO message = new MailMessageDTO()
            {
                Mail = pMail,
                Title = AppConstant.MailTitleDefault,
                Body = string.Format(AppConstant.MailBodyDefault, pMessage),
                IsPriority = false
            };
            this.SendMailWithAddressApp(message);
        }
        #endregion
    }
}
