using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Repositories;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Services
{
    public class ApplicationService
    {
        #region Variables
        private readonly ILogger<ApplicationService> _logger;

        private readonly ApplicationRepository _repository;
        #endregion

        #region Constructor
        public ApplicationService(ILogger<ApplicationService> logger, ApplicationRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        #endregion

        #region Methods
        public async Task<ReturnDTO> GetByApplication()
        {
            _logger.LogInformation($"ApplicationService.GetByApplication => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetByApplication();
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"ApplicationService.GetByApplication => Exception: { ex.Message }");
            }
            _logger.LogInformation($"ApplicationService.GetByApplication => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
