using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Entities;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using Sample.ElectronicCommerce.Shared.Services;
using System;
using System.Collections.Generic;
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
        ) {
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
                ReturnDTO returnDTO = await this.GetByMail(pEntity.Mail);
                if (returnDTO.IsSuccess)
                {
                    responseDTO = new ResponseDTO(false, "E-mail já cadastrado no sistemas", null);
                }
                else
                {
                    responseDTO = await _repository.InsertAsync(pEntity);
                }
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserService.InsertAsync => Exception: { ex.Message }");
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
                _logger.LogError($"UserService.UpdateAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "UserService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserService.UpdateAsync => End");
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
                _logger.LogError($"UserService.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation($"UserService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetByMail(string pMail)
        {
            _logger.LogInformation($"UserService.GetByMail => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetByMail(pMail);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserService.GetByMail => Exception: { ex.Message }");
            }
            _logger.LogInformation($"UserService.GetByMail => End");
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
                _logger.LogError($"UserService.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation($"UserService.GetAll => End");
            return new ReturnDTO(responseDTO);
        }        
        #endregion
    }
}
