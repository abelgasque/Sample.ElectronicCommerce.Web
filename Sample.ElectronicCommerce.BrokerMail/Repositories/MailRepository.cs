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
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerMail.Repositories
{
    public class MailRepository
    {
        #region Variables
        private readonly ILogger<MailRepository> _logger;
        
        private readonly BrokerMailSettings _brokerMailSettings;

        private readonly IMongoCollection<MailEntity> _collection;
        #endregion

        #region Constructor
        public MailRepository(
            ILogger<MailRepository> logger,
            IOptions<BrokerMailSettings> brokerMailSettings
        ) {
            _logger = logger;
            _brokerMailSettings = brokerMailSettings.Value;            
            var mongoClient = new MongoClient(_brokerMailSettings.MongoClient.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_brokerMailSettings.MongoClient.DataBase);
            _collection = mongoDatabase.GetCollection<MailEntity>(_brokerMailSettings.MailColletion);              
        }
        #endregion

        #region Methods

        public async Task<ResponseDTO> InsertAsync(MailEntity pEntity)
        {
            _logger.LogInformation("MailRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtCreation = DateTime.Now;       
                await _collection.InsertOneAsync(pEntity);
                _logger.LogInformation("MailRepository.InsertAsync => OK");
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailRepository.InsertAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(MailEntity pEntity)
        {
            _logger.LogInformation("MailRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<MailEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pEntity.Id));
                MailEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
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
                _logger.LogError($"MailRepository.UpdateAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("MailRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<MailEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
                MailEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                bool isSuccess = (entity != null) ? true : false;
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailRepository.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("MailRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<MailEntity, bool>> filter = x => x.IsActive.Equals(true);
                ICollection<MailEntity> listEntities = await _collection.Find(filter).ToListAsync();                
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailRepository.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
