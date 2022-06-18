using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Mail.Repositories;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Services;
using Sample.ElectronicCommerce.Core.Util;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Mail.Services
{
    public class MailSingleService
    {
        #region Variables
        private readonly ILogger<MailSingleService> _logger;

        private readonly AppSettings _appSettings;

        private readonly MailSingleRepository _repository;
        
        private readonly MailBrokerRepository _mailBrokerRepository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public MailSingleService(
            ILogger<MailSingleService> logger, 
            IOptions<AppSettings> appSettings, 
            MailSingleRepository repository, 
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
        public async Task<ReturnDTO> InsertAsync(MailSingleEntity pEntity)
        {
            _logger.LogInformation($"MailSingleService.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.InsertAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailSingleService.InsertAsync => Exception: {ex.Message}");
            }
            await _logAppService.AppInsertAsync(0, "MailSingleService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailSingleService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(MailSingleEntity pEntity)
        {
            _logger.LogInformation($"MailSingleService.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.UpdateAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailSingleService.UpdateAsync => Exception: {ex.Message}");
            }
            await _logAppService.AppInsertAsync(0, "MailSingleService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailSingleService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"MailSingleService.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetById(pId);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailSingleService.GetById => Exception: {ex.Message}");
            }
            _logger.LogInformation($"MailSingleService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation("MailSingleService.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetAll();
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailSingleService.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation("MailSingleService.GetAll > Finish");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
