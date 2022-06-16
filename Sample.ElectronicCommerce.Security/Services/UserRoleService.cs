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
    public class UserRoleService
    {
        #region Variables
        private readonly ILogger<UserRoleService> _logger;

        private readonly UserRoleRepository _repository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructors
        public UserRoleService(
            ILogger<UserRoleService> logger,
            UserRoleRepository repository,
            LogAppService logAppService
        )
        {
            _logger = logger;
            _repository = repository;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods  
        public async Task<ReturnDTO> InsertAsync(UserRoleEntity pEntity)
        {
            _logger.LogInformation($"UserRoleService.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.InsertAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRoleService.InsertAsync => Exception: {ex.Message}");
            }
            await _logAppService.AppInsertAsync(0, "UserRoleService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserRoleService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(UserRoleEntity pEntity)
        {
            _logger.LogInformation($"UserRoleService.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.UpdateAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRoleService.UpdateAsync => Exception: {ex.Message}");
            }
            await _logAppService.AppInsertAsync(0, "UserRoleService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserRoleService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> DeleteAsync(string pId)
        {
            _logger.LogInformation($"UserRoleService.DeleteAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.DeleteAsync(pId);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRoleService.DeleteAsync => Exception: {ex.Message}");
            }
            await _logAppService.AppInsertAsync(0, "UserRoleService.DeleteAsync", pId, responseDTO);
            _logger.LogInformation($"UserRoleService.DeleteAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"UserRoleService.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetById(pId);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRoleService.GetById => Exception: {ex.Message}");
            }
            _logger.LogInformation($"UserRoleService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation("UserRoleService.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetAll();
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRoleService.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation("UserRoleService.GetAll > Finish");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
