using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Constants;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using MongoDB.Bson;
using Sample.ElectronicCommerce.Security.Entities;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;

namespace Sample.ElectronicCommerce.Security.Repositories
{
    public class UserSessionRepository
    {
        #region Variables
        private readonly ILogger<UserSessionRepository> _logger;
        
        private readonly AppMongoClient.MongoClientSettings _mongoClientSettings;
        
        private readonly IMongoCollection<UserSessionEntity> _collection;
        #endregion

        #region Constructor
        public UserSessionRepository(
            ILogger<UserSessionRepository> logger,
            IOptions<AppMongoClient.MongoClientSettings> mongoClientSettings
        ) {
            _logger = logger;
            _mongoClientSettings = mongoClientSettings.Value;
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBaseProduction);
            _collection = mongoDatabase.GetCollection<UserSessionEntity>(_mongoClientSettings.UserSessionColletion);
        }
        #endregion

        #region Methods
        public async Task<ResponseDTO> InsertAsync(UserSessionEntity pEntity)
        {
            _logger.LogInformation("UserSessionRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtCreation = DateTime.Now;
                await _collection.InsertOneAsync(pEntity);
                _logger.LogInformation("UserSessionRepository.InsertAsync => OK");
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.InsertAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserSessionRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(UserSessionEntity pEntity)
        {
            _logger.LogInformation("UserSessionRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<UserSessionEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
                UserSessionEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
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
                _logger.LogError($"UserSessionRepository.UpdateAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserSessionRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("UserSessionRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<UserSessionEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
                UserSessionEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                bool isSuccess = (entity != null) ? true : false;
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserSessionRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetByIdUser(string pIdUser)
        {
            _logger.LogInformation("UserSessionRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<UserSessionEntity, bool>> filter = x => x.IdUser.Equals(pIdUser);
                UserSessionEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                bool isSuccess = (entity != null) ? true : false;
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.GetById => Exception: {ex.Message}");
            }
            _logger.LogInformation("UserSessionRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("UserSessionRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<UserSessionEntity, bool>> filter = x => x.IsActive == true;
                List<UserSessionEntity> listEntities = await _collection.Find(filter).ToListAsync();                
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserSessionRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
