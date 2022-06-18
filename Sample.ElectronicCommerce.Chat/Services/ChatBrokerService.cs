using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Chat.Repositories;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Chat.Services
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
            List<ChatMessageEntity> result;
            try
            {
                result = await _repository.GetAll();
                _logger.LogInformation($"ChatBrokerService.GetAll => OK => Count: {result.Count}");
            }
            catch (Exception ex)
            {
                result = new List<ChatMessageEntity>();
                _logger.LogError($"ChatBrokerService.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation("ChatBrokerService.GetAll => End");
            return result;
        }


        public async Task AppInsertAsync(ChatMessageEntity pEntity)
        {
            _logger.LogInformation("ChatBrokerService.AppInsertAsync => Start");
            try
            {
                await _repository.Insert(pEntity);
                _logger.LogInformation("ChatBrokerService.AppInsertAsync => OK");
            }
            catch (Exception ex)
            {
                _logger.LogError($"ChatBrokerService.AppInsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("ChatBrokerService.AppInsertAsync => End");
        }
        #endregion
    }
}
