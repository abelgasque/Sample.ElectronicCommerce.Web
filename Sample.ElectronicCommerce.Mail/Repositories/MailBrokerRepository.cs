using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Util;

namespace Sample.ElectronicCommerce.Mail.Repositories
{
    public class MailBrokerRepository
    {
        #region Variables
        private readonly ILogger<MailBrokerRepository> _logger;

        private readonly AppMongoClient.MongoClientSettings _mongoClientSettings;

        private readonly IMongoCollection<MailBrokerEntity> _collection;
        #endregion

        #region Constructor
        public MailBrokerRepository(
            ILogger<MailBrokerRepository> logger,
            IOptions<AppMongoClient.MongoClientSettings> mongoClientSettings
        )
        {
            _logger = logger;
            _mongoClientSettings = mongoClientSettings.Value;
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBase);
            _collection = mongoDatabase.GetCollection<MailBrokerEntity>(_mongoClientSettings.MailBrokerColletion);
        }
        #endregion

        #region Methods Crud
        public async Task<ResponseDTO> InsertAsync(MailBrokerEntity pEntity)
        {
            _logger.LogInformation("MailBrokerRepository.InsertAsync => Start");
            pEntity.DtCreation = DateTime.Now;
            await _collection.InsertOneAsync(pEntity);
            _logger.LogInformation("MailBrokerRepository.InsertAsync => OK");
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            _logger.LogInformation("MailBrokerRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(MailBrokerEntity pEntity)
        {
            _logger.LogInformation("MailBrokerRepository.UpdateAsync => Start");
            Expression<Func<MailBrokerEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
            MailBrokerEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            bool isSuccess = (entity != null) ? true : false;
            string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            object dataObject = (isSuccess) ? pEntity : null;
            if (isSuccess)
            {
                pEntity.DtLastUpdate = DateTime.Now;
                await _collection.ReplaceOneAsync(filter, pEntity);
            }
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            _logger.LogInformation("MailBrokerRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("MailBrokerRepository.GetById => Start");
            Expression<Func<MailBrokerEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
            MailBrokerEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            bool isSuccess = (entity != null) ? true : false;
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            _logger.LogInformation("MailBrokerRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("MailBrokerRepository.GetAll => Start");
            Expression<Func<MailBrokerEntity, bool>> filter = x => x.IsActive == true;
            List<MailBrokerEntity> listEntities = await _collection.Find(filter).ToListAsync();
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            _logger.LogInformation("MailBrokerRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}