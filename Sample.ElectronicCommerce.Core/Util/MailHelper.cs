using Sample.ElectronicCommerce.Core.Entities.DTO;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Util
{
    public class MailHelper
    {

        #region Methods Sending Mail
        // public async Task<ReturnDTO> MultipleMailSending(List<MailSingleEntity> pListEntity)
        // {
        //     ReturnDTO returnDTO;
        //     bool isSuccess = (pListEntity != null && pListEntity.Count > 0) ? true : false;
        //     string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
        //     object dataObject = (isSuccess) ? pListEntity : null;
        //     if (pListEntity != null && pListEntity.Count > 0)
        //     {
        //         foreach (MailSingleEntity entity in pListEntity)
        //         {
        //             returnDTO = await this.SendMailAsync(entity);
        //             if (!returnDTO.IsSuccess)
        //             {
        //                 isSuccess = returnDTO.IsSuccess;
        //                 deMessage = returnDTO.DeMessage;
        //                 dataObject = returnDTO.ResultObject;
        //                 break;
        //             }
        //         }
        //     }
        //     return new ReturnDTO(isSuccess, deMessage, dataObject);
        // }

        // public async Task<ReturnDTO> SingleMailSending(MailSingleEntity pEntity)
        // {
        //     return await this.SendMailAsync(pEntity);
        // }

        private async Task<ReturnDTO> SendMailAsync(
            MailAddressDTO pMailAddress,
            MailMessageDTO pMailMessage
        )
        {
            //_logger.LogInformation("MailHelper.SendMailAsync => Start");
            ResponseDTO responseDTO;
            MailAddress toAddress = new MailAddress(pMailMessage.Mail);
            MailAddress fromAddress = new MailAddress(pMailAddress.UserName);
            var message = new MailMessage(fromAddress, toAddress);
            message.Subject = pMailMessage.Title;
            message.Body = pMailMessage.Body;
            message.IsBodyHtml = true;
            message.HeadersEncoding = Encoding.UTF8;
            message.SubjectEncoding = Encoding.UTF8;
            message.BodyEncoding = Encoding.UTF8;
            if (pMailMessage.IsPriority) message.Priority = MailPriority.High;
            SmtpClient client = new SmtpClient(pMailAddress.Server);
            if (pMailAddress.Port != null) client = new SmtpClient(pMailAddress.Server, pMailAddress.Port.Value);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.EnableSsl = pMailAddress.IsEnabledSsl;
            client.UseDefaultCredentials = false;
            NetworkCredential smtpUserInfo = new NetworkCredential(pMailAddress.UserName, pMailAddress.Password);
            client.Credentials = smtpUserInfo;
            await client.SendMailAsync(message);
            client.Dispose();
            message.Dispose();
            responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, null);
            //await _logAppService.AppInsertAsync(0, "MailHelper.SendMailAsync", pEntity, responseDTO);
            //_logger.LogInformation("MailHelper.SendMailAsync => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
