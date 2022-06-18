using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Repositories;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System;
using System.Threading.Tasks;
using Sample.ElectronicCommerce.Core.Util;

namespace Sample.ElectronicCommerce.Core.Services
{
    public class OrganizationService
    {
        #region Variables
        private readonly ILogger<OrganizationService> _logger;

        private readonly OrganizationRepository _repository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public OrganizationService(
            ILogger<OrganizationService> logger, 
            OrganizationRepository repository,
            LogAppService logAppService
        ) {
            _logger = logger;
            _repository = repository;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods  
        public async Task<ReturnDTO> InsertAsync(OrganizationEntity pEntity)
        {
            _logger.LogInformation($"OrganizationService.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.InsertAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"OrganizationService.InsertAsync => Exception: {ex.Message}");
            }
            await _logAppService.AppInsertAsync(0, "OrganizationService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"OrganizationService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(OrganizationEntity pEntity)
        {
            _logger.LogInformation($"OrganizationService.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.UpdateAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"OrganizationService.UpdateAsync => Exception: {ex.Message}");
            }
            await _logAppService.AppInsertAsync(0, "OrganizationService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"OrganizationService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"OrganizationService.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetById(pId);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"OrganizationService.GetById => Exception: {ex.Message}");
            }
            _logger.LogInformation($"OrganizationService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation("OrganizationService.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetAll();
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"OrganizationService.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation("OrganizationService.GetAll > Finish");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
