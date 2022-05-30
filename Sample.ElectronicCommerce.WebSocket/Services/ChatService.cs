using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.WebSocket.Entities;
using Sample.ElectronicCommerce.WebSocket.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.WebSocket.Services
{
    public class ChatService
    {
        #region Variables
        private readonly ILogger<ChatService> _logger;

        private readonly ChatRepository _repository;
        #endregion

        #region Constructors
        public ChatService(
            ILogger<ChatService> logger,
            ChatRepository repository
        )
        {
            _logger = logger;
            _repository = repository;
        }
        #endregion

        #region Methods
        public async Task<List<Message>> GetAllAsync()
        {
            _logger.LogInformation("ChatService.GetAllAsync => Start");
            List<Message> result;
            try
            {
                result = await _repository.GetAllAsync();
                _logger.LogInformation($"ChatService.GetAllAsync => OK => Count: {result.Count}");
            }
            catch (Exception ex)
            {
                result = new List<Message>();
                _logger.LogError($"ChatService.GetAllAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("ChatService.GetAllAsync => End");
            return result;
        }

        public async Task AppInsertAsync(string pUserName, string pText)
        {
            _logger.LogInformation("ChatService.AppInsertAsync => Start");
            try
            {
                Message newMessage = new Message()
                {
                    UserName = pUserName,
                    Text = pText
                };
                await _repository.InsertAsync(newMessage);
                _logger.LogInformation("ChatService.AppInsertAsync => OK");
            }
            catch (Exception ex)
            {
                _logger.LogError($"ChatService.AppInsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("ChatService.AppInsertAsync => End");
        }
        #endregion
    }
}
