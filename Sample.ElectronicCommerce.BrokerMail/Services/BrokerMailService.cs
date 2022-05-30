using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.BrokerMail.Entities;
using Sample.ElectronicCommerce.BrokerMail.Repositories;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using Sample.ElectronicCommerce.Shared.Services;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerMail.Services
{
    public class BrokerMailService
    {
        #region Variables
        private readonly ILogger<BrokerMailService> _logger;

        private readonly BrokerMailRepository _repository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public BrokerMailService(
            ILogger<BrokerMailService> logger, 
            BrokerMailRepository repository, 
            LogAppService logAppService
        ) {
            _logger = logger;
            _repository = repository;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods
        public async Task<ReturnDTO> InsertAsync(BrokerMailEntity pEntity)
        {
            _logger.LogInformation($"BrokerMailService.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.InsertAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"BrokerMailService.InsertAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "BrokerMailService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"BrokerMailService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(BrokerMailEntity pEntity)
        {
            _logger.LogInformation($"BrokerMailService.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {                
                responseDTO = await _repository.UpdateAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"BrokerMailService.UpdateAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "BrokerMailService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"BrokerMailService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"BrokerMailService.GetById => Start");
            ResponseDTO responseDTO;
            try
            {                
                responseDTO = await _repository.GetById(pId);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"BrokerMailService.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation($"BrokerMailService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation($"BrokerMailService.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetAll();
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"BrokerMailService.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation($"BrokerMailService.GetAll => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
