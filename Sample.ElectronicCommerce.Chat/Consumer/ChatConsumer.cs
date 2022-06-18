using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Chat.Services;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Chat.Consumer
{
    public class ChatConsumer : Hub
    {
        #region Variables
        private readonly ILogger<ChatConsumer> _logger;

        private readonly ChatBrokerService _service;
        #endregion

        #region Constructors
        public ChatConsumer(
            ILogger<ChatConsumer> logger,
            ChatBrokerService service
        ) {
            _logger = logger;
            _service = service;
        }
        #endregion
        
        #region Methods
        public async Task SendMessageChatBrokerAll(ChatMessageEntity pEntity)
        {
            _logger.LogInformation("ChatHub.SendMessageChatBrokerAll => Start");
            try
            {
                await Clients.All.SendAsync("ReceiveMessageChatBrokerAll", pEntity, pEntity.Message);
                await _service.AppInsertAsync(pEntity);
                _logger.LogInformation("ChatHub.SendMessageChatBrokerAll => OK");
            } 
            catch(Exception ex)
            {
                _logger.LogError($"ChatHub.SendMessageChatBrokerAll => Exception: { ex.Message }");
            }
        }
        #endregion
    }
}