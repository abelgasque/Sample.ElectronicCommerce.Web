using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.WebSocket.Entities;
using Sample.ElectronicCommerce.WebSocket.Repositories;
using Sample.ElectronicCommerce.WebSocket.Services;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.WebSocket.Hubs
{
    public class ChatHub : Hub
    {
        #region Variables
        private readonly ILogger<ChatHub> _logger;

        private readonly ChatService _service;
        #endregion

        #region Constructors
        public ChatHub(
            ILogger<ChatHub> logger,
            ChatService service
        ) {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region Methods
        public async Task SendMessage(string pUserName, string pText)
        {
            _logger.LogInformation("ChatHub.SendMessage => Start");
            try
            {
                await Clients.All.SendAsync("ReceiveMessage", pUserName, pText);
                await _service.AppInsertAsync(pUserName, pText);
                _logger.LogInformation("ChatHub.SendMessage => OK");
            } 
            catch(Exception ex)
            {
                _logger.LogError($"ChatHub.SendMessage => Exception: { ex.Message }");
            }
        }
        #endregion
    }
}