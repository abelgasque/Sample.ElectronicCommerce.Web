using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Util;

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
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBase);
            _collection = mongoDatabase.GetCollection<UserEntity>(_mongoClientSettings.UserColletion);
        }
        #endregion

        #region Methods Crud
        public async Task<ResponseDTO> InsertAsync(UserEntity pEntity)
        {
            _logger.LogInformation("UserRepository.InsertAsync => Start");
            await _collection.InsertOneAsync(pEntity);
            _logger.LogInformation("UserRepository.InsertAsync => OK");
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            _logger.LogInformation("UserRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(UserEntity pEntity)
        {
            _logger.LogInformation("UserRepository.UpdateAsync => Start");
            Expression<Func<UserEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
            UserEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            bool isSuccess = (entity != null) ? true : false;
            string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            object dataObject = (isSuccess) ? pEntity : null;
            if (isSuccess)
            {
                await _collection.ReplaceOneAsync(filter, pEntity);
            }
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            _logger.LogInformation("UserRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> DeleteAsync(string pId)
        {
            _logger.LogInformation("UserRepository.DeleteAsync => Start");
            Expression<Func<UserEntity, bool>> filter = x => x.Id.Equals(pId);
            UserEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            bool isSuccess = (entity != null) ? true : false;
            string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            if (isSuccess)
            {
                await _collection.FindOneAndDeleteAsync(pId);
            }
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, null);
            _logger.LogInformation("UserRepository.DeleteAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("UserRepository.GetById => Start");
            Expression<Func<UserEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
            UserEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            bool isSuccess = (entity != null) ? true : false;
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            _logger.LogInformation("UserRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetByMail(string pMail)
        {
            _logger.LogInformation("UserRepository.GetByMail => Start");
            Expression<Func<UserEntity, bool>> filter = x => x.Mail.Equals(pMail);
            UserEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            bool isSuccess = (entity != null) ? true : false;
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            _logger.LogInformation("UserRepository.GetByMail > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("UserRepository.GetAll => Start");
            List<UserEntity> listEntities = await _collection.Aggregate().ToListAsync();
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            _logger.LogInformation("UserRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}