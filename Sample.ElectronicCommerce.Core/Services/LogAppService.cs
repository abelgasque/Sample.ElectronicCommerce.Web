using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sample.ElectronicCommerce.Core.Entities.DataBase.Mapping;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Repositories;
using Sample.ElectronicCommerce.Core.Util;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Services
{
    public class LogAppService
    {
        #region Variables
        private readonly ILogger<LogAppService> _logger;

        private readonly AppSettings _appSettings;

        private readonly LogAppRepository _repository;
        #endregion

        #region Constructor
        public LogAppService(
            ILogger<LogAppService> logger, 
            IOptions<AppSettings> appSettings, 
            LogAppRepository logAppRepository
        ) {
            _logger = logger;
            _appSettings = appSettings.Value;
            _repository = logAppRepository;
        }
        #endregion

        #region Methods
        public async Task<ReturnDTO> AppInsertAsync(string pIdUser, string pMethod, object pContent, ResponseDTO pResponseDTO)
        {
            _logger.LogInformation($"LogAppService.AppInsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                LogAppEntity entity = new LogAppEntity()
                {
                    IdUser = pIdUser,
                    IdOrganization = null,
                    DtCreation = null,
                    DtLastUpdate = null,
                    Version = _appSettings.Version,
                    Method = pMethod,
                    Content = (pContent != null) ? JsonConvert.SerializeObject(pContent) : null,
                    IsDebug = _appSettings.IsDebug,
                    IsActive = true,
                };

                if (pResponseDTO != null)
                {
                    entity.Result = (pResponseDTO.DataObject != null) ? JsonConvert.SerializeObject(pResponseDTO.DataObject) : null;
                    entity.Message = (!string.IsNullOrEmpty(pResponseDTO.DeMessage)) ? pResponseDTO.DeMessage : null;
                    entity.ExceptionMessage = (!string.IsNullOrEmpty(pResponseDTO.DeExceptionMessage)) ? pResponseDTO.DeExceptionMessage : null;
                    entity.StackTrace = (!string.IsNullOrEmpty(pResponseDTO.DeStackTrace) ? pResponseDTO.DeStackTrace : null);
                    entity.IsSuccess = pResponseDTO.IsSuccess;
                }

                responseDTO = await _repository.InsertAsync(entity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppService.AppInsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation($"LogAppService.AppInsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> InsertAsync(LogAppEntity pEntity)
        {
            _logger.LogInformation($"LogAppService.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.InsertAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppService.InsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation($"LogAppService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(LogAppEntity pEntity)
        {
            _logger.LogInformation($"LogAppService.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtLastUpdate = DateTime.Now;
                responseDTO = await _repository.UpdateAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppService.UpdateAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation($"LogAppService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"LogAppService.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetById(pId);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppService.GetById => Exception: {ex.Message}");
            }
            _logger.LogInformation($"LogAppService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation($"LogAppService.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetAll();
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppService.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation($"LogAppService.GetAll => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
