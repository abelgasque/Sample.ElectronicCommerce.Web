using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Util;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;

namespace Sample.ElectronicCommerce.Mail.Repositories
{
    public class MailGroupRepository
    {
        #region Variables
        private readonly ILogger<MailGroupRepository> _logger;
        
        private readonly AppMongoClient.MongoClientSettings _mongoClientSettings;

        private readonly IMongoCollection<MailGroupEntity> _collection;
        #endregion

        #region Constructor
        public MailGroupRepository(
            ILogger<MailGroupRepository> logger,
            IOptions<AppMongoClient.MongoClientSettings> mongoClientSettings
        ) {
            _logger = logger;
            _mongoClientSettings = mongoClientSettings.Value;            
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBaseProduction);
            _collection = mongoDatabase.GetCollection<MailGroupEntity>(_mongoClientSettings.MailGroupColletion);              
        }
        #endregion

        #region Methods Crud
        public async Task<ResponseDTO> InsertAsync(MailGroupEntity pEntity)
        {
            _logger.LogInformation("MailGroupRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtCreation = DateTime.Now;
                await _collection.InsertOneAsync(pEntity);
                _logger.LogInformation("MailGroupRepository.InsertAsync => OK");
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailGroupRepository.InsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("MailGroupRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(MailGroupEntity pEntity)
        {
            _logger.LogInformation("MailGroupRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<MailGroupEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
                MailGroupEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
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
                _logger.LogError($"MailGroupRepository.UpdateAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("MailGroupRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("MailGroupRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<MailGroupEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
                MailGroupEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                bool isSuccess = (entity != null) ? true : false;
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailGroupRepository.GetById => Exception: {ex.Message}");
            }
            _logger.LogInformation("MailGroupRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("MailGroupRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<MailGroupEntity, bool>> filter = x => x.IsActive == true;
                List<MailGroupEntity> listEntities = await _collection.Find(filter).ToListAsync();
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailGroupRepository.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation("MailGroupRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
