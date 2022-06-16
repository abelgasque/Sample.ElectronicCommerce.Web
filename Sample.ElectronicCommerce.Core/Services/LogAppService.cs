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
        public async Task<ReturnDTO> AppInsertAsync(long pIdUserSession, string pNmMethod, object pContent, ResponseDTO pResponseDTO)
        {
            _logger.LogInformation($"LogAppService.AppInsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                LogAppEntity entity = new LogAppEntity()
                {
                    IdUserSession = pIdUserSession,
                    IdApplication = 0,
                    DtCreation = DateTime.Now,
                    DtLastUpdate = null,
                    NuVersion = _appSettings.Version,
                    NmMethod = pNmMethod,
                    DeContent = (pContent != null) ? JsonConvert.SerializeObject(pContent) : null,
                    IsTest = false,
                    IsActive = true,
                };

                if (pResponseDTO != null)
                {
                    entity.DeResult = (pResponseDTO.DataObject != null) ? JsonConvert.SerializeObject(pResponseDTO.DataObject) : null;
                    entity.DeMessage = (!string.IsNullOrEmpty(pResponseDTO.DeMessage)) ? pResponseDTO.DeMessage : null;
                    entity.DeExceptionMessage = (!string.IsNullOrEmpty(pResponseDTO.DeExceptionMessage)) ? pResponseDTO.DeExceptionMessage : null;
                    entity.DeStackTrace = (!string.IsNullOrEmpty(pResponseDTO.DeStackTrace) ? pResponseDTO.DeStackTrace : null);
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

        public async Task<ReturnDTO> GetById(long pId)
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

        public async Task<ReturnDTO> GetAll(bool? pIsActive)
        {
            _logger.LogInformation($"LogAppService.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetAll(pIsActive);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppService.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation($"LogAppService.GetAll => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetLogAppForChartDynamic(bool pMustFilterYear)
        {
            _logger.LogInformation($"LogAppService.GetLogAppForChartDynamic => Start => pMustFilterYear: {pMustFilterYear}");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetLogAppForChartDynamic(pMustFilterYear);
                _logger.LogInformation($"LogAppService.GetLogAppForChartDynamic => sqlConnectionResponse.IsSuccess: {responseDTO.IsSuccess}");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppService.GetLogAppForChartDynamic => Exception: {ex.Message}");
            }
            _logger.LogInformation($"LogAppService.GetLogAppForChartDynamic => End => pMustFilterYear: {pMustFilterYear}");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetLogAppDay()
        {
            _logger.LogInformation($"LogAppService.GetLogAppDay => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetLogAppDay();
                _logger.LogInformation($"LogAppService.GetLogAppDay => sqlConnectionResponse.IsSuccess: {responseDTO.IsSuccess}");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"LogAppService.GetLogAppDay => Exception: {ex.Message}");
            }
            _logger.LogInformation($"LogAppService.GetLogAppDay => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
