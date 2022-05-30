using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using Sample.ElectronicCommerce.WebSocket.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.WebSocket.Repositories
{
    public class ChatRepository
    {
        #region Variables
        private readonly ILogger<ChatRepository> _logger;
        private readonly WebSocketSettings _webSocketSettings;
        private readonly IMongoCollection<Message> _collection;
        #endregion

        #region Constructors
        public ChatRepository(
            ILogger<ChatRepository> logger,
            IOptions<WebSocketSettings> webSocketSettings
        ) {
            _logger = logger;
            _webSocketSettings = webSocketSettings.Value;
            var mongoClient = new MongoClient(_webSocketSettings.MongoClient.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_webSocketSettings.MongoClient.DataBase);
            _collection = mongoDatabase.GetCollection<Message>(_webSocketSettings.MongoClient.DocMessage);
        }
        #endregion

        #region Methods
        public async Task<List<Message>> GetAllAsync() 
        {
            _logger.LogInformation("ChatRepository.GetAllAsync => Start");
            List<Message> result;
            try
            {
                result = await _collection.Find(_ => true).ToListAsync();
                _logger.LogInformation($"ChatRepository.GetAllAsync => OK => Count: { result.Count }");              
            }
            catch (Exception ex)
            {
                result = new List<Message>();
                _logger.LogError($"ChatRepository.GetAllAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("ChatRepository.GetAllAsync => End");
            return result;
        }                

        public async Task InsertAsync(Message pEntity)
        {
            _logger.LogInformation("ChatRepository.InsertAsync => Start");
            try
            {
                await _collection.InsertOneAsync(pEntity);
                _logger.LogInformation("ChatRepository.InsertAsync => OK");
            }
            catch (Exception ex)
            {
                _logger.LogError($"ChatRepository.InsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("ChatRepository.InsertAsync => End");
        }
        #endregion
    }
}
