using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;

namespace Sample.ElectronicCommerce.Core.Repositories
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
        )
        {
            _logger = logger;
            _mongoClientSettings = mongoClientSettings.Value;
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBase);
            _collection = mongoDatabase.GetCollection<ChatMessageEntity>(_mongoClientSettings.ChatBrokerAllColletion);
        }
        #endregion

        #region Methods
        public async Task<List<ChatMessageEntity>> GetAll()
        {
            _logger.LogInformation("ChatBrokerRepository.GetAll => Start");
            List<ChatMessageEntity> result = await _collection.Find(_ => true).ToListAsync();
            _logger.LogInformation($"ChatBrokerRepository.GetAll => Count: {result.Count} => End");
            return result;
        }

        public async Task Insert(ChatMessageEntity pEntity)
        {
            _logger.LogInformation("ChatBrokerRepository.InsertAsync => Start");
            await _collection.InsertOneAsync(pEntity);
            _logger.LogInformation("ChatBrokerRepository.InsertAsync => End");
        }
        #endregion
    }
}