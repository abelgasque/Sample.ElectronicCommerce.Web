using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.BrokerChat.Entities;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerChat.Services
{
    public class ChatBrokerAllConsumer : Hub
    {
        #region Variables
        private readonly ILogger<ChatBrokerAllConsumer> _logger;

        private readonly ChatBrokerService _service;
        #endregion

        #region Constructors
        public ChatBrokerAllConsumer(
            ILogger<ChatBrokerAllConsumer> logger,
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