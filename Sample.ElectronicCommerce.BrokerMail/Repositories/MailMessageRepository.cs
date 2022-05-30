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
    public class MailMessageRepository
    {
        #region Variables
        private readonly ILogger<MailMessageRepository> _logger;

        private readonly BrokerMailSettings _brokerMailSettings;

        private readonly IMongoCollection<MailMessageEntity> _collection;
        #endregion

        #region Constructor
        public MailMessageRepository(
            ILogger<MailMessageRepository> logger,            
            IOptions<BrokerMailSettings> brokerMailSettings
        ) {
            _logger = logger;
            _brokerMailSettings = brokerMailSettings.Value;            
            var mongoClient = new MongoClient(_brokerMailSettings.MongoClient.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_brokerMailSettings.MongoClient.DataBase);
            _collection = mongoDatabase.GetCollection<MailMessageEntity>(_brokerMailSettings.MailMessageColletion);            
        }
        #endregion

        #region Methods
        public async Task<ResponseDTO> InsertAsync(MailMessageEntity pEntity)
        {
            _logger.LogInformation("MailMessageRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtCreation = DateTime.Now;       
                await _collection.InsertOneAsync(pEntity);
                _logger.LogInformation("MailMessageRepository.InsertAsync => OK");
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageRepository.InsertAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailMessageRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(MailMessageEntity pEntity)
        {
            _logger.LogInformation("MailMessageRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<MailMessageEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pEntity.Id));
                MailMessageEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
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
                _logger.LogError($"MailMessageRepository.UpdateAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailMessageRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("MailMessageRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<MailMessageEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
                MailMessageEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                bool isSuccess = (entity != null) ? true : false;
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageRepository.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailMessageRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("MailMessageRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<MailMessageEntity, bool>> filter = x => x.IsActive.Equals(true);
                ICollection<MailMessageEntity> listEntities = await _collection.Find(filter).ToListAsync();                
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageRepository.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailMessageRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
