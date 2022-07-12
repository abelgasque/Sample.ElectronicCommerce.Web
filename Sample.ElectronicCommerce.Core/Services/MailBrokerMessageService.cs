using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Repositories;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Services;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Services
{
    public class MailBrokerMessageService
    {
        #region Variables
        private readonly ILogger<MailBrokerMessageService> _logger;

        private readonly MailBrokerMessageRepository _repository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public MailBrokerMessageService(
            ILogger<MailBrokerMessageService> logger,
            MailBrokerMessageRepository repository,
            LogAppService logAppService
        )
        {
            _logger = logger;
            _repository = repository;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods  
        public async Task<ReturnDTO> InsertAsync(MailGroupEntity pEntity)
        {
            _logger.LogInformation($"MailBrokerMessageService.InsertAsync => Start");
            ResponseDTO responseDTO = await _repository.InsertAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "MailBrokerMessageService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailBrokerMessageService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(MailGroupEntity pEntity)
        {
            _logger.LogInformation($"MailBrokerMessageService.UpdateAsync => Start");
            ResponseDTO responseDTO = await _repository.UpdateAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "MailBrokerMessageService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailBrokerMessageService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"MailBrokerMessageService.GetById => Start");
            ResponseDTO responseDTO = await _repository.GetById(pId);
            _logger.LogInformation($"MailBrokerMessageService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation("MailBrokerMessageService.GetAll => Start");
            ResponseDTO responseDTO = await _repository.GetAll();
            _logger.LogInformation("MailBrokerMessageService.GetAll > Finish");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
