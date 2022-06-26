using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Repositories;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Services
{
    public class OrganizationService
    {
        #region Variables
        private readonly ILogger<OrganizationService> _logger;

        private readonly OrganizationRepository _repository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public OrganizationService(
            ILogger<OrganizationService> logger,
            OrganizationRepository repository,
            LogAppService logAppService
        )
        {
            _logger = logger;
            _repository = repository;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods  
        public async Task<ReturnDTO> InsertAsync(OrganizationEntity pEntity)
        {
            _logger.LogInformation($"OrganizationService.InsertAsync => Start");
            ResponseDTO responseDTO = await _repository.InsertAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "Organization.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"OrganizationService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(OrganizationEntity pEntity)
        {
            _logger.LogInformation($"OrganizationService.UpdateAsync => Start");
            ResponseDTO responseDTO = await _repository.UpdateAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "Organization.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"OrganizationService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"OrganizationService.GetById => Start");
            return new ReturnDTO(await _repository.GetById(pId));
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation("OrganizationService.GetAll => Start");
            return new ReturnDTO(await _repository.GetAll());
        }
        #endregion
    }
}