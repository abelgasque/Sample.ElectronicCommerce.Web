using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.BrokerChat.Entities;
using Sample.ElectronicCommerce.BrokerChat.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerChat.Services
{
    public class BrokerChatService
    {
        #region Variables
        private readonly ILogger<BrokerChatService> _logger;

        private readonly BrokerChatRepository _repository;
        #endregion

        #region Constructors
        public BrokerChatService(
            ILogger<BrokerChatService> logger,
            BrokerChatRepository repository
        )
        {
            _logger = logger;
            _repository = repository;
        }
        #endregion

        #region Methods
        public async Task<List<BrokerChatEntity>> GetAll()
        {
            _logger.LogInformation("BrokerChatService.GetAll => Start");
            List<BrokerChatEntity> result;
            try
            {
                result = await _repository.GetAll();
                _logger.LogInformation($"BrokerChatService.GetAll => OK => Count: {result.Count}");
            }
            catch (Exception ex)
            {
                result = new List<BrokerChatEntity>();
                _logger.LogError($"BrokerChatService.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation("BrokerChatService.GetAll => End");
            return result;
        }

        public async Task AppInsertAsync(string pUserName, string pText)
        {
            _logger.LogInformation("BrokerChatService.AppInsertAsync => Start");
            try
            {
                BrokerChatEntity newMessage = new BrokerChatEntity()
                {
                    UserName = pUserName,
                    Text = pText
                };
                await _repository.Insert(newMessage);
                _logger.LogInformation("BrokerChatService.AppInsertAsync => OK");
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrokerChatService.AppInsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("BrokerChatService.AppInsertAsync => End");
        }
        #endregion
    }
}
