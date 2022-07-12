using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Services;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Consumer
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
        )
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region Methods
        public async Task SendMessageChatBrokerAll(ChatMessageEntity pEntity)
        {
            _logger.LogInformation("ChatHub.SendMessageChatBrokerAll => Start");
            await Clients.All.SendAsync("ReceiveMessageChatBrokerAll", pEntity, pEntity.Message);
            await _service.AppInsertAsync(pEntity);
            _logger.LogInformation("ChatHub.SendMessageChatBrokerAll => OK");
        }
        #endregion
    }
}