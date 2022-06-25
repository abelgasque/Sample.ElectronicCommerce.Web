using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Mail.Repositories;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Services;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Mail.Services
{
    public class MailBrokerService
    {
        #region Variables
        private readonly ILogger<MailBrokerService> _logger;

        private readonly MailBrokerRepository _repository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public MailBrokerService(
            ILogger<MailBrokerService> logger,
            MailBrokerRepository repository,
            LogAppService logAppService
        )
        {
            _logger = logger;
            _repository = repository;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods  
        public async Task<ReturnDTO> InsertAsync(MailBrokerEntity pEntity)
        {
            _logger.LogInformation($"MailBrokerService.InsertAsync => Start");
            ResponseDTO responseDTO = await _repository.InsertAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "MailBrokerService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailBrokerService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(MailBrokerEntity pEntity)
        {
            _logger.LogInformation($"MailBrokerService.UpdateAsync => Start");
            ResponseDTO responseDTO = await _repository.UpdateAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "MailBrokerService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailBrokerService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"MailBrokerService.GetById => Start");
            ResponseDTO responseDTO = await _repository.GetById(pId);
            _logger.LogInformation($"MailBrokerService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation("MailBrokerService.GetAll => Start");
            ResponseDTO responseDTO = await _repository.GetAll();
            _logger.LogInformation("MailBrokerService.GetAll > Finish");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}