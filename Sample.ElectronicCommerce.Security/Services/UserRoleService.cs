using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Services
{
    public class UserRoleService
    {
        #region Variables
        private readonly ILogger<UserRoleService> _logger;

        private readonly UserRoleRepository _repository;
        #endregion

        #region Constructor
        public UserRoleService(ILogger<UserRoleService> logger, UserRoleRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        #endregion

        #region Methods        
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
                _logger.LogError($"UserRoleService.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserRoleService.GetAll > Finish");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
