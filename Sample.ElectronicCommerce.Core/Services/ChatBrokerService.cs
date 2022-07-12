using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Repositories;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Services
{
    public class ChatBrokerService
    {
        #region Variables
        private readonly ILogger<ChatBrokerService> _logger;

        private readonly ChatBrokerRepository _repository;
        #endregion

        #region Constructors
        public ChatBrokerService(
            ILogger<ChatBrokerService> logger,
            ChatBrokerRepository repository
        )
        {
            _logger = logger;
            _repository = repository;
        }
        #endregion

        #region Methods
        public async Task<List<ChatMessageEntity>> GetAll()
        {
            _logger.LogInformation("ChatBrokerService.GetAll => Start");
            List<ChatMessageEntity> result = await _repository.GetAll();
            _logger.LogInformation($"ChatBrokerService.GetAll => End => Count: {result.Count}");
            return result;
        }


        public async Task AppInsertAsync(ChatMessageEntity pEntity)
        {
            _logger.LogInformation("ChatBrokerService.AppInsertAsync => Start");
            await _repository.Insert(pEntity);
            _logger.LogInformation("ChatBrokerService.AppInsertAsync => End");
        }
        #endregion
    }
}