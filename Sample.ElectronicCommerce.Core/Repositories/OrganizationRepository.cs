using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Sample.ElectronicCommerce.Core.Entities.MongoDb;
using Sample.ElectronicCommerce.Core.Constants;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;

namespace Sample.ElectronicCommerce.Core.Repositories
{
    public class OrganizationRepository
    {
        #region Variables
        private readonly ILogger<OrganizationRepository> _logger;

        private readonly AppMongoClient.MongoClientSettings _mongoClientSettings;
        
        private readonly IMongoCollection<OrganizationEntity> _collection;
        #endregion

        #region Constructor
        public OrganizationRepository(
            ILogger<OrganizationRepository> logger,
            IOptions<AppMongoClient.MongoClientSettings> mongoClientSettings
        ) {
            _logger = logger;
            _mongoClientSettings = mongoClientSettings.Value;
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBaseProduction);
            _collection = mongoDatabase.GetCollection<OrganizationEntity>(_mongoClientSettings.OrganizationColletion);
        }
        #endregion

        #region Methods Crud
        public async Task<ResponseDTO> InsertAsync(OrganizationEntity pEntity)
        {
            _logger.LogInformation("OrganizationRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtCreation = DateTime.Now;
                await _collection.InsertOneAsync(pEntity);
                _logger.LogInformation("OrganizationRepository.InsertAsync => OK");
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"OrganizationRepository.InsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("OrganizationRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(OrganizationEntity pEntity)
        {
            _logger.LogInformation("OrganizationRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<OrganizationEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
                OrganizationEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
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
                _logger.LogError($"OrganizationRepository.UpdateAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("OrganizationRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("OrganizationRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<OrganizationEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
                OrganizationEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                bool isSuccess = (entity != null) ? true : false;
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"OrganizationRepository.GetById => Exception: {ex.Message}");
            }
            _logger.LogInformation("OrganizationRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("OrganizationRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<OrganizationEntity, bool>> filter = x => x.IsActive == true;
                List<OrganizationEntity> listEntities = await _collection.Find(filter).ToListAsync();
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"OrganizationRepository.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation("OrganizationRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
