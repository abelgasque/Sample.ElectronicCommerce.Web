using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using MongoDB.Bson;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Util;

namespace Sample.ElectronicCommerce.Security.Repositories
{
    public class UserRoleRepository
    {
        #region Variables
        private readonly ILogger<UserRoleRepository> _logger;

        private readonly AppMongoClient.MongoClientSettings _mongoClientSettings;

        private readonly IMongoCollection<UserRoleEntity> _collection;
        #endregion

        #region Constructor
        public UserRoleRepository(
            ILogger<UserRoleRepository> logger,
            IOptions<AppMongoClient.MongoClientSettings> mongoClientSettings
        )
        {
            _logger = logger;
            _mongoClientSettings = mongoClientSettings.Value;
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBase);
            _collection = mongoDatabase.GetCollection<UserRoleEntity>(_mongoClientSettings.UserRoleColletion);
        }
        #endregion

        #region Methods Crud
        public async Task<ResponseDTO> InsertAsync(UserRoleEntity pEntity)
        {
            _logger.LogInformation("UserRoleRepository.InsertAsync => Start");
            pEntity.DtCreation = DateTime.Now;
            await _collection.InsertOneAsync(pEntity);
            _logger.LogInformation("UserRoleRepository.InsertAsync => OK");
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            _logger.LogInformation("UserRoleRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(UserRoleEntity pEntity)
        {
            _logger.LogInformation("UserRoleRepository.UpdateAsync => Start");
            Expression<Func<UserRoleEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
            UserRoleEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            bool isSuccess = (entity != null) ? true : false;
            string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            object dataObject = (isSuccess) ? pEntity : null;
            if (isSuccess)
            {
                pEntity.DtLastUpdate = DateTime.Now;
                await _collection.ReplaceOneAsync(filter, pEntity);
            }
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            _logger.LogInformation("UserRoleRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> DeleteAsync(string pId)
        {
            _logger.LogInformation("UserRoleRepository.DeleteAsync => Start");
            Expression<Func<UserRoleEntity, bool>> filter = x => x.Id.Equals(pId);
            UserRoleEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            bool isSuccess = (entity != null) ? true : false;
            string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            if (isSuccess)
            {
                await _collection.FindOneAndDeleteAsync(pId);
            }
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, null);
            _logger.LogInformation("UserRoleRepository.DeleteAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("UserRoleRepository.GetById => Start");
            Expression<Func<UserRoleEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
            UserRoleEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            bool isSuccess = (entity != null) ? true : false;
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            _logger.LogInformation("UserRoleRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("UserRoleRepository.GetAll => Start");
            Expression<Func<UserRoleEntity, bool>> filter = x => x.IsActive == true;
            List<UserRoleEntity> listEntities = await _collection.Find(filter).ToListAsync();
            _logger.LogInformation($"UserRoleRepository.GetAll => Entities.Count: {listEntities.Count}");
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            _logger.LogInformation("UserRoleRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
