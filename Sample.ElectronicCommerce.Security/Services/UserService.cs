using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Entities;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Core.Constants;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Services;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Services
{
    public class UserService
    {
        #region Variables
        private readonly ILogger<UserService> _logger;

        private readonly UserRepository _repository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public UserService(
            ILogger<UserService> logger,
            UserRepository repository,
            LogAppService logAppService
        )
        {
            _logger = logger;
            _repository = repository;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods        
        public async Task<ReturnDTO> InsertAsync(UserEntity pEntity)
        {
            _logger.LogInformation($"UserService.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.InsertAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserService.InsertAsync => Exception: {ex.Message}");
            }
            await _logAppService.AppInsertAsync(0, "UserService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(UserEntity pEntity)
        {
            _logger.LogInformation($"UserService.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.UpdateAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserService.UpdateAsync => Exception: {ex.Message}");
            }
            await _logAppService.AppInsertAsync(0, "UserService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> DeleteAsync(string pId)
        {
            _logger.LogInformation($"UserService.DeleteAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.DeleteAsync(pId);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserService.DeleteAsync => Exception: {ex.Message}");
            }
            await _logAppService.AppInsertAsync(0, "UserService.DeleteAsync", pId, responseDTO);
            _logger.LogInformation($"UserService.DeleteAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"UserService.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetById(pId);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserService.GetById => Exception: {ex.Message}");
            }
            _logger.LogInformation($"UserService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation($"UserService.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetAll();
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserService.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation($"UserService.GetAll => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
