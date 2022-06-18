using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Util
{
    public class MailHelper
    {
        
        #region Methods Sending Mail
        public async Task<ReturnDTO> MultipleMailSending(List<MailSingleEntity> pListEntity)
        {
            ReturnDTO returnDTO;
            bool isSuccess = (pListEntity != null && pListEntity.Count > 0) ? true : false;
            string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            object dataObject = (isSuccess) ? pListEntity : null;
            if (pListEntity != null && pListEntity.Count > 0)
            {
                foreach (MailSingleEntity entity in pListEntity)
                {
                    returnDTO = await this.SendMailAsync(entity);
                    if (!returnDTO.IsSuccess)
                    {
                        isSuccess = returnDTO.IsSuccess;
                        deMessage = returnDTO.DeMessage;
                        dataObject = returnDTO.ResultObject;
                        break;
                    }
                }
            }
            return new ReturnDTO(isSuccess, deMessage, dataObject);
        }

        public async Task<ReturnDTO> SingleMailSending(MailSingleEntity pEntity)
        {
            return await this.SendMailAsync(pEntity);
        }

        private async Task<ReturnDTO> SendMailAsync(MailSingleEntity pEntity)
        {
            return null;
            // _logger.LogInformation("MailHelper.SendMailAsync => Start");
            // ResponseDTO responseDTO;
            // try
            // {
            //     responseDTO = await _mailBrokerRepository.GetById(pEntity.IdMailBroker);
            //     MailBrokerEntity mailBroker = (MailBrokerEntity)responseDTO.DataObject;

            //     if (mailBroker == null)
            //     {
            //         responseDTO = new ResponseDTO(false, AppConstant.DeErrorMessageMailBrokerWS, null);
            //     }
            //     else
            //     {
            //         MailAddress toAddress = new MailAddress(pEntity.Mail);
            //         MailAddress fromAddress = new MailAddress(mailBroker.UserName);
            //         var message = new MailSingleEntity(fromAddress, toAddress);

            //         message.Subject = pEntity.Title;
            //         message.Body = pEntity.Body;
            //         message.IsBodyHtml = true;
            //         message.HeadersEncoding = Encoding.UTF8;
            //         message.SubjectEncoding = Encoding.UTF8;
            //         message.BodyEncoding = Encoding.UTF8;
            //         if (pEntity.IsPriority) { message.Priority = MailPriority.High; }

            //         SmtpClient client = new SmtpClient(mailBroker.Server);

            //         if (mailBroker.Port != null)
            //         {
            //             client = new SmtpClient(mailBroker.Server, mailBroker.Port.Value);
            //         }

            //         client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //         client.EnableSsl = mailBroker.IsEnabledSsl;
            //         client.UseDefaultCredentials = false;

            //         NetworkCredential smtpUserInfo = new NetworkCredential(mailBroker.UserName, mailBroker.Password);
            //         client.Credentials = smtpUserInfo;

            //         await client.SendMailAsync(message);

            //         client.Dispose();
            //         message.Dispose();
            //         responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, null);
            //     }
            // }
            // catch (Exception ex)
            // {
            //     _logger.LogError($"MailHelper.SendMailAsync => Exception: {ex.Message}");
            //     responseDTO = new ResponseDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
            // }
            // await _logAppService.AppInsertAsync(0, "MailHelper.SendMailAsync", pEntity, responseDTO);
            // _logger.LogInformation("MailHelper.SendMailAsync => End");
            // return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
