using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Sample.ElectronicCommerce.BrokerChat.Entities;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerChat.Repositories
{
    public class BrokerChatRepository
    {
        #region Variables
        private readonly ILogger<BrokerChatRepository> _logger;
        private readonly BrokerChatSettings _brokerChatSettings;
        private readonly IMongoCollection<BrokerChatEntity> _collection;
        #endregion

        #region Constructors
        public BrokerChatRepository(
            ILogger<BrokerChatRepository> logger,
            IOptions<BrokerChatSettings> brokerChatSettings
        ) {
            _logger = logger;
            _brokerChatSettings = brokerChatSettings.Value;
            var mongoClient = new MongoClient(_brokerChatSettings.MongoClient.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_brokerChatSettings.MongoClient.DataBase);
            _collection = mongoDatabase.GetCollection<BrokerChatEntity>(_brokerChatSettings.BrokerChatColletion);
        }
        #endregion

        #region Methods
        public async Task<List<BrokerChatEntity>> GetAll() 
        {
            _logger.LogInformation("BrokerChatRepository.GetAll => Start");
            List<BrokerChatEntity> result;
            try
            {
                result = await _collection.Find(_ => true).ToListAsync();
                _logger.LogInformation($"BrokerChatRepository.GetAll => OK => Count: { result.Count }");              
            }
            catch (Exception ex)
            {
                result = new List<BrokerChatEntity>();
                _logger.LogError($"BrokerChatRepository.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation("BrokerChatRepository.GetAll => End");
            return result;
        }                

        public async Task Insert(BrokerChatEntity pEntity)
        {
            _logger.LogInformation("BrokerChatRepository.InsertAsync => Start");
            try
            {
                await _collection.InsertOneAsync(pEntity);
                _logger.LogInformation("BrokerChatRepository.InsertAsync => OK");
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrokerChatRepository.InsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("BrokerChatRepository.InsertAsync => End");
        }
        #endregion
    }
}
