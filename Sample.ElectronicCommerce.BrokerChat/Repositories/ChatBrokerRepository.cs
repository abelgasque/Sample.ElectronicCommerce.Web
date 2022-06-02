using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Sample.ElectronicCommerce.BrokerChat.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;

namespace Sample.ElectronicCommerce.BrokerChat.Repositories
{
    public class ChatBrokerRepository
    {
        #region Variables
        private readonly ILogger<ChatBrokerRepository> _logger;
        private readonly AppMongoClient.MongoClientSettings _mongoClientSettings;
        private readonly IMongoCollection<ChatMessageEntity> _collection;
        #endregion

        #region Constructors
        public ChatBrokerRepository(
            ILogger<ChatBrokerRepository> logger,
            IOptions<AppMongoClient.MongoClientSettings> mongoClientSettings
        ) {
            _logger = logger;
            _mongoClientSettings = mongoClientSettings.Value;
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBaseProduction);
            _collection = mongoDatabase.GetCollection<ChatMessageEntity>(_mongoClientSettings.ChatBrokerAllColletion);
        }
        #endregion

        #region Methods
        public async Task<List<ChatMessageEntity>> GetAll() 
        {
            _logger.LogInformation("ChatBrokerRepository.GetAll => Start");
            List<ChatMessageEntity> result;
            try
            {
                result = await _collection.Find(_ => true).ToListAsync();
                _logger.LogInformation($"ChatBrokerRepository.GetAll => OK => Count: { result.Count }");              
            }
            catch (Exception ex)
            {
                result = new List<ChatMessageEntity>();
                _logger.LogError($"ChatBrokerRepository.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation("ChatBrokerRepository.GetAll => End");
            return result;
        }                

        public async Task Insert(ChatMessageEntity pEntity)
        {
            _logger.LogInformation("ChatBrokerRepository.InsertAsync => Start");
            try
            {
                await _collection.InsertOneAsync(pEntity);
                _logger.LogInformation("ChatBrokerRepository.InsertAsync => OK");
            }
            catch (Exception ex)
            {
                _logger.LogError($"ChatBrokerRepository.InsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("ChatBrokerRepository.InsertAsync => End");
        }
        #endregion
    }
}
