using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Services;
using System;
using System.Threading.Tasks;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;

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
            pEntity.DtCreation = DateTime.Now;
            ResponseDTO responseDTO = await _repository.InsertAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "UserRoleService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserRoleService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(UserRoleEntity pEntity)
        {
            _logger.LogInformation($"UserRoleService.UpdateAsync => Start");
            pEntity.DtLastUpdate = DateTime.Now;
            ResponseDTO responseDTO = await _repository.UpdateAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "UserRoleService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserRoleService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> DeleteAsync(string pId)
        {
            _logger.LogInformation($"UserRoleService.DeleteAsync => Start");
            ResponseDTO responseDTO = await _repository.DeleteAsync(pId);
            await _logAppService.AppInsertAsync(null, "UserRoleService.DeleteAsync", pId, responseDTO);
            _logger.LogInformation($"UserRoleService.DeleteAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"UserRoleService.GetById => Start");
            ResponseDTO responseDTO = await _repository.GetById(pId);
            _logger.LogInformation($"UserRoleService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation("UserRoleService.GetAll => Start");
            ResponseDTO responseDTO = await _repository.GetAll();
            _logger.LogInformation("UserRoleService.GetAll > Finish");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}