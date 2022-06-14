using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.BrokerChat.Services;
using Sample.ElectronicCommerce.Core.Entities.MongoDb;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerChat.Consumer
{
    public class ChatBrokerConsumer : Hub
    {
        #region Variables
        private readonly ILogger<ChatBrokerConsumer> _logger;

        private readonly ChatBrokerService _service;
        #endregion

        #region Constructors
        public ChatBrokerConsumer(
            ILogger<ChatBrokerConsumer> logger,
            ChatBrokerService service
        ) {
            _logger = logger;
            _service = service;
        }
        #endregion
        
        #region Methods
        public async Task SendMessageChatBrokerAll(ChatMessageEntity pEntity)
        {
            _logger.LogInformation("BrokerChatHub.SendMessageChatBrokerAll => Start");
            try
            {
                await Clients.All.SendAsync("ReceiveMessageChatBrokerAll", pEntity, pEntity.Message);
                await _service.AppInsertAsync(pEntity);
                _logger.LogInformation("BrokerChatHub.SendMessageChatBrokerAll => OK");
            } 
            catch(Exception ex)
            {
                _logger.LogError($"BrokerChatHub.SendMessageChatBrokerAll => Exception: { ex.Message }");
            }
        }
        #endregion
    }
}