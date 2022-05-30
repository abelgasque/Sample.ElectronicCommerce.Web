using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.BrokerChat.Services;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerChat.Hubs
{
    public class BrokerChatHub : Hub
    {
        #region Variables
        private readonly ILogger<BrokerChatHub> _logger;

        private readonly BrokerChatService _service;
        #endregion

        #region Constructors
        public BrokerChatHub(
            ILogger<BrokerChatHub> logger,
            BrokerChatService service
        ) {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region Methods
        public async Task SendMessageBroker(string pUserName, string pText)
        {
            _logger.LogInformation("BrokerChatHub.SendMessageBroker => Start");
            try
            {
                await Clients.All.SendAsync("ReceiveMessageBroker", pUserName, pText);
                await _service.AppInsertAsync(pUserName, pText);
                _logger.LogInformation("BrokerChatHub.SendMessageBroker => OK");
            } 
            catch(Exception ex)
            {
                _logger.LogError($"BrokerChatHub.SendMessageBroker => Exception: { ex.Message }");
            }
        }
        #endregion
    }
}