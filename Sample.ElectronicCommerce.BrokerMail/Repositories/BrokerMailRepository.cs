using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Sample.ElectronicCommerce.BrokerMail.Entities;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerMail.Repositories
{
    public class BrokerMailRepository
    {
        #region Variables
        private readonly ILogger<BrokerMailRepository> _logger;

        private readonly BrokerMailSettings _brokerMailSettings;

        private readonly IMongoCollection<BrokerMailEntity> _collection;
        #endregion

        #region Constructor
        public BrokerMailRepository(
            ILogger<BrokerMailRepository> logger, 
            IOptions<BrokerMailSettings> brokerMailSettings
        ) {
            _logger = logger;
            _brokerMailSettings = brokerMailSettings.Value;            
            var mongoClient = new MongoClient(_brokerMailSettings.MongoClient.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_brokerMailSettings.MongoClient.DataBase);
            _collection = mongoDatabase.GetCollection<BrokerMailEntity>(_brokerMailSettings.BrokerMailColletion);
        }
        #endregion

        #region Methods
        public async Task<ResponseDTO> InsertAsync(BrokerMailEntity pEntity)
        {
            _logger.LogInformation("BrokerMailRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtCreation = DateTime.Now;       
                await _collection.InsertOneAsync(pEntity);
                _logger.LogInformation("BrokerMailRepository.InsertAsync => OK");
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"BrokerMailRepository.InsertAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("BrokerMailRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(BrokerMailEntity pEntity)
        {
            _logger.LogInformation("BrokerMailRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<BrokerMailEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pEntity.Id));
                BrokerMailEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                bool isSuccess = (entity != null) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                if (isSuccess)
                {   
                    pEntity.DtLastUpdate = DateTime.Now;
                    await _collection.ReplaceOneAsync(filter, pEntity); 
                }
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"BrokerMailRepository.UpdateAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("BrokerMailRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("BrokerMailRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<BrokerMailEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
                BrokerMailEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                bool isSuccess = (entity != null) ? true : false;
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"BrokerMailRepository.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation("BrokerMailRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("BrokerMailRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {               
                Expression<Func<BrokerMailEntity, bool>> filter = x => x.IsActive.Equals(true);
                ICollection<BrokerMailEntity> listEntities = await _collection.Find(filter).ToListAsync();                
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"BrokerMailRepository.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("BrokerMailRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
