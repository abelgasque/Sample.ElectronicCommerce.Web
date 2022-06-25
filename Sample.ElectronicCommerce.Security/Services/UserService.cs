using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Services;
using System;
using System.Threading.Tasks;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;

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
            pEntity.DtCreation = DateTime.Now;
            ResponseDTO responseDTO = await _repository.InsertAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "UserService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(UserEntity pEntity)
        {
            _logger.LogInformation($"UserService.UpdateAsync => Start");
            pEntity.DtLastUpdate = DateTime.Now;
            ResponseDTO responseDTO = await _repository.UpdateAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "UserService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> DeleteAsync(string pId)
        {
            _logger.LogInformation($"UserService.DeleteAsync => Start");
            ResponseDTO responseDTO = await _repository.DeleteAsync(pId);
            await _logAppService.AppInsertAsync(null, "UserService.DeleteAsync", pId, responseDTO);
            _logger.LogInformation($"UserService.DeleteAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"UserService.GetById => Start");
            ResponseDTO responseDTO = await _repository.GetById(pId);
            _logger.LogInformation($"UserService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetByMail(string pMail)
        {
            _logger.LogInformation($"UserService.GetByMail => Start");
            ResponseDTO responseDTO = await _repository.GetByMail(pMail);
            _logger.LogInformation($"UserService.GetByMail => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation($"UserService.GetAll => Start");
            ResponseDTO responseDTO = await _repository.GetAll();
            _logger.LogInformation($"UserService.GetAll => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion

        #region Methods User Lead
        public async Task<ReturnDTO> UserLeadInsertAsync(UserLeadDTO pEntity)
        {
            _logger.LogInformation($"UserService.UserLeadInsertAsync => Start");
            UserEntity entity = new UserEntity()
            {
                Name = pEntity.Name,
                Mail = pEntity.Mail,
                NuCellPhone = pEntity.Phone,
                Code = Guid.NewGuid().ToString().Substring(0, 8),
                IsBlock = true
            };
            ResponseDTO responseDTO = await _repository.InsertAsync(entity);
            _logger.LogInformation($"UserService.UserLeadInsertAsync => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}