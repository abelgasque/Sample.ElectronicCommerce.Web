using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping;
using Sample.ElectronicCommerce.BrokerMail.Repositories;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using Sample.ElectronicCommerce.Shared.Helpers;
using Sample.ElectronicCommerce.Shared.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerMail.Services
{
    public class MailMessageService
    {
        #region Variables
        private readonly ILogger<MailMessageService> _logger;

        private readonly AppSettings _appSettings;

        private readonly MailMessageRepository _repository;
        
        private readonly MailBrokerRepository _mailBrokerRepository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public MailMessageService(
            ILogger<MailMessageService> logger, 
            IOptions<AppSettings> appSettings, 
            MailMessageRepository repository, 
            MailBrokerRepository mailBrokerRepository,
            LogAppService logAppService
        ) {
            _logger = logger;
            _appSettings = appSettings.Value;
            _repository = repository;
            _mailBrokerRepository = mailBrokerRepository;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods
        public async Task<ReturnDTO> InsertAsync(MailMessageEntity pEntity)
        {
            _logger.LogInformation($"MailMessageService.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtCreation = DateTime.Now;
                pEntity.NuVersion = _appSettings.Version;
                responseDTO = await _repository.InsertAsync(pEntity);
                if (responseDTO.IsSuccess)
                {                    
                    ReturnDTO returnDTO = await this.SingleMailSending(pEntity);
                    responseDTO = new ResponseDTO(returnDTO.IsSuccess, returnDTO.DeMessage, returnDTO.ResultObject);
                }
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageService.InsertAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "MailMessageService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailMessageService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(MailMessageEntity pEntity)
        {
            _logger.LogInformation($"MailMessageService.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtLastUpdate = DateTime.Now;
                responseDTO = await _repository.UpdateAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageService.UpdateAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "MailMessageService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailMessageService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(long pId)
        {
            _logger.LogInformation($"MailMessageService.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetById(pId);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageService.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation($"MailMessageService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll(bool? pIsActive)
        {
            _logger.LogInformation($"MailMessageService.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetAll(pIsActive);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageService.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation($"MailMessageService.GetAll => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion     

        #region Methods Sending Mail
        public async Task<ReturnDTO> MultipleMailSending(List<MailMessageEntity> pListEntity)
        {
            ReturnDTO returnDTO;
            bool isSuccess = (pListEntity != null && pListEntity.Count > 0) ? true : false;
            string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            object dataObject = (isSuccess) ? pListEntity : null;
            if (pListEntity != null && pListEntity.Count > 0)
            {
                foreach (MailMessageEntity entity in pListEntity)
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

        public async Task<ReturnDTO> SingleMailSending(MailMessageEntity pEntity)
        {
            return await this.SendMailAsync(pEntity);
        }

        private async Task<ReturnDTO> SendMailAsync(MailMessageEntity pEntity)
        {
            _logger.LogInformation("MailHelper.SendMailAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _mailBrokerRepository.GetById(pEntity.IdMailBroker);
                MailBrokerEntity mailBroker = (MailBrokerEntity)responseDTO.DataObject;

                if (mailBroker == null)
                {
                    responseDTO = new ResponseDTO(false, AppConstant.DeErrorMessageMailBrokerWS, null);
                }
                else
                {
                    MailAddress toAddress = new MailAddress(pEntity.Mail);
                    MailAddress fromAddress = new MailAddress(mailBroker.UserName);
                    var message = new MailMessage(fromAddress, toAddress);

                    message.Subject = pEntity.Title;
                    message.Body = pEntity.Body;
                    message.IsBodyHtml = true;
                    message.HeadersEncoding = Encoding.UTF8;
                    message.SubjectEncoding = Encoding.UTF8;
                    message.BodyEncoding = Encoding.UTF8;
                    if (pEntity.IsPriority) { message.Priority = MailPriority.High; }

                    SmtpClient client = new SmtpClient(mailBroker.Server);

                    if (mailBroker.Port != null)
                    {
                        client = new SmtpClient(mailBroker.Server, mailBroker.Port.Value);
                    }

                    client.DeliveryMethod = SmtpDeliveryMethod.Network;
                    client.EnableSsl = mailBroker.IsEnabledSsl;
                    client.UseDefaultCredentials = false;

                    NetworkCredential smtpUserInfo = new NetworkCredential(mailBroker.UserName, mailBroker.Password);
                    client.Credentials = smtpUserInfo;

                    await client.SendMailAsync(message);

                    client.Dispose();
                    message.Dispose();
                    responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, null);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailHelper.SendMailAsync => Exception: {ex.Message}");
                responseDTO = new ResponseDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex.Message.ToString(), ex.StackTrace.ToString(), null);
            }
            await _logAppService.AppInsertAsync(0, "MailHelper.SendMailAsync", pEntity, responseDTO);
            _logger.LogInformation("MailHelper.SendMailAsync => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
