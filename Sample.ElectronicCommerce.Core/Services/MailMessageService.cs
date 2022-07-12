using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Core.Repositories;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Services;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Services
{
    public class MailMessageService
    {
        #region Variables
        private readonly ILogger<MailMessageService> _logger;

        private readonly AppSettings _appSettings;

        private readonly MailMessageRepository _repository;

        private readonly MailBrokerRepository _mailBrokerRepository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public MailMessageService(
            ILogger<MailMessageService> logger,
            IOptions<AppSettings> appSettings,
            MailMessageRepository repository,
            MailBrokerRepository mailBrokerRepository,
            LogAppService logAppService
        )
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _repository = repository;
            _mailBrokerRepository = mailBrokerRepository;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods  
        public async Task<ReturnDTO> InsertAsync(MailSingleEntity pEntity)
        {
            _logger.LogInformation($"MailMessageService.InsertAsync => Start");
            ResponseDTO responseDTO = await _repository.InsertAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "MailMessageService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailMessageService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(MailSingleEntity pEntity)
        {
            _logger.LogInformation($"MailMessageService.UpdateAsync => Start");
            ResponseDTO responseDTO = await _repository.UpdateAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "MailMessageService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailMessageService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"MailMessageService.GetById => Start");
            ResponseDTO responseDTO = await _repository.GetById(pId);
            _logger.LogInformation($"MailMessageService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation("MailMessageService.GetAll => Start");
            ResponseDTO responseDTO = await _repository.GetAll();
            _logger.LogInformation("MailMessageService.GetAll > Finish");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
