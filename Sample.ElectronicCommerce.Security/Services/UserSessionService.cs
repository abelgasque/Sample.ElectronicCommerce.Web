using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Security.Entities;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Core.Constants;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Services;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Services
{
    public class UserSessionService
    {
        #region Variables
        private readonly ILogger<UserSessionService> _logger;
        
        private readonly SecuritySettings _securitySettings;

        private readonly AppSettings _appSettings;

        private readonly UserSessionRepository _repository;        

        private readonly UserService _userService;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public UserSessionService(
            ILogger<UserSessionService> logger, 
            IOptions<SecuritySettings> securitySettings, 
            IOptions<AppSettings> appSettings, 
            UserSessionRepository repository, 
            UserService userService, 
            LogAppService logAppService
        ) {
            _logger = logger;
            _securitySettings = securitySettings.Value;
            _appSettings = appSettings.Value;
            _repository = repository;
            _userService = userService;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods        
        public async Task<ReturnDTO> InsertAsync(UserSessionEntity pEntity)
        {
            _logger.LogInformation($"UserSessionService.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtCreation = DateTime.Now;
                responseDTO = await _repository.InsertAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionService.InsertAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "UserSessionService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserSessionService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(UserSessionEntity pEntity)
        {
            _logger.LogInformation($"UserSessionService.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtLastUpdate = DateTime.Now;
                responseDTO = await _repository.UpdateAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionService.UpdateAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "UserSessionService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserSessionService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"UserSessionService.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetById(pId);
                if (responseDTO.IsSuccess)
                {
                    UserSessionEntity entity = (UserSessionEntity)responseDTO.DataObject;
                    ReturnDTO returnDTO = await _userService.GetById(entity.IdUser.ToString());
                    if (returnDTO.IsSuccess && responseDTO.DataObject != null)
                    {
                        UserEntity user = (UserEntity)returnDTO.ResultObject;
                        entity.User = user;
                    }
                }                
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionService.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation($"UserSessionService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation($"UserSessionService.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetAll();
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionService.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation($"UserSessionService.GetAll => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}