using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Repositories;
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
        )
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _repository = logAppRepository;
        }
        #endregion

        #region End Points
        public async Task<ReturnDTO> InsertAsync(LogAppEntity pEntity)
        {
            _logger.LogInformation($"LogAppService.InsertAsync => Start");
            return new ReturnDTO(await _repository.InsertAsync(pEntity));
        }

        public async Task<ReturnDTO> UpdateAsync(LogAppEntity pEntity)
        {
            _logger.LogInformation($"LogAppService.UpdateAsync => Start");
            pEntity.DtLastUpdate = DateTime.Now;
            return new ReturnDTO(await _repository.UpdateAsync(pEntity));
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"LogAppService.GetById => Start");
            return new ReturnDTO(await _repository.GetById(pId));
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation($"LogAppService.GetAll => Start");
            return new ReturnDTO(await _repository.GetAll());
        }
        #endregion

        #region Methods
        public async Task<ReturnDTO> AppInsertAsync(string pIdUser, string pMethod, object pContent, ResponseDTO pResponseDTO)
        {
            _logger.LogInformation($"LogAppService.AppInsertAsync => Start");
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

            return new ReturnDTO(await _repository.InsertAsync(entity));
        }
        #endregion
    }
}