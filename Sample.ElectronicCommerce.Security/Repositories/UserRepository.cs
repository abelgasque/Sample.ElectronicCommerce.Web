using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Sample.ElectronicCommerce.Security.Entities;
using Sample.ElectronicCommerce.Core.Constants;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;

namespace Sample.ElectronicCommerce.Security.Repositories
{
    public class UserRepository
    {
        #region Variables
        private readonly ILogger<UserRepository> _logger;

        private readonly AppMongoClient.MongoClientSettings _mongoClientSettings;

        private readonly IMongoCollection<UserEntity> _collection;
        #endregion

        #region Constructor
        public UserRepository(
            ILogger<UserRepository> logger,
            IOptions<AppMongoClient.MongoClientSettings> mongoClientSettings
        )
        {
            _logger = logger;
            _mongoClientSettings = mongoClientSettings.Value;
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBaseProduction);
            _collection = mongoDatabase.GetCollection<UserEntity>(_mongoClientSettings.UserColletion);
        }
        #endregion

        #region Methods
        public async Task<ResponseDTO> InsertAsync(UserEntity pEntity)
        {
            _logger.LogInformation("UserRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                await _collection.InsertOneAsync(pEntity);
                _logger.LogInformation("UserRepository.InsertAsync => OK");
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.InsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("UserRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(UserEntity pEntity)
        {
            _logger.LogInformation("UserRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<UserEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
                UserEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                bool isSuccess = (entity != null) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                if (isSuccess)
                {
                    await _collection.ReplaceOneAsync(filter, pEntity);
                }
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.UpdateAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("UserRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> DeleteAsync(string pId)
        {
            _logger.LogInformation("UserRepository.DeleteAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<UserEntity, bool>> filter = x => x.Id.Equals(pId);
                UserEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                bool isSuccess = (entity != null) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                if (isSuccess)
                {
                    await _collection.FindOneAndDeleteAsync(pId);
                }
                responseDTO = new ResponseDTO(isSuccess, deMessage, null);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.DeleteAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("UserRepository.DeleteAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("UserRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<UserEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
                UserEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                bool isSuccess = (entity != null) ? true : false;
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.GetById => Exception: {ex.Message}");
            }
            _logger.LogInformation("UserRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("UserRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                List<UserEntity> listEntities = await _collection.Aggregate().ToListAsync();
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation("UserRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
