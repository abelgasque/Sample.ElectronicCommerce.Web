using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using MongoDB.Bson;
using Sample.ElectronicCommerce.Security.Entities;

namespace Sample.ElectronicCommerce.Security.Repositories
{
    public class UserSessionRepository
    {
        #region Variables
        private readonly ILogger<UserSessionRepository> _logger;
        
        private readonly SecuritySettings _securitySettings;
        
        private readonly IMongoCollection<UserSessionEntity> _collection;
        #endregion

        #region Constructor
        public UserSessionRepository(
            ILogger<UserSessionRepository> logger,
            IOptions<SecuritySettings> securitySettings
        ) {
            _logger = logger;
            _securitySettings = securitySettings.Value;
            var mongoClient = new MongoClient(_securitySettings.MongoClient.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_securitySettings.MongoClient.DataBase);
            _collection = mongoDatabase.GetCollection<UserSessionEntity>(_securitySettings.UserSessionColletion);
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
                Expression<Func<UserSessionEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pEntity.Id));
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

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("UserSessionRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<UserSessionEntity, bool>> filter = x => x.IsActive.Equals(true);
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

        public async Task<ResponseDTO> GetByIdUserWithAuthFailed(string pIdUser)
        {
            _logger.LogInformation("UserSessionRepository.GetByIdUserWithAuthFailed => Start");
            ResponseDTO responseDTO;
            try
            {                
                Expression<Func<UserSessionEntity, bool>> filter = x => (x.IdUser.Equals(pIdUser) 
                                                                            && x.IsSuccess.Equals(false) 
                                                                            && x.DtCreation >= DateTime.Now.AddMinutes(-5) 
                                                                            && x.DtCreation <= DateTime.Now);
                UserSessionEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                bool isSuccess = (entity != null) ? true : false;
                _logger.LogInformation("UserSessionRepository.GetByIdUserWithAuthFailed => OK");
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.GetByIdUserWithAuthFailed => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserSessionRepository.GetByIdUserWithAuthFailed > Finish");
            return responseDTO;
        }
        #endregion
    }
}
