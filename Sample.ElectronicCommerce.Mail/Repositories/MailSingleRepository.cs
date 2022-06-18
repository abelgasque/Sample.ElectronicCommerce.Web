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
    public class MailSingleRepository
    {
        #region Variables
        private readonly ILogger<MailSingleRepository> _logger;

        private readonly AppMongoClient.MongoClientSettings _mongoClientSettings;

        private readonly IMongoCollection<MailSingleEntity> _collection;
        #endregion

        #region Constructor
        public MailSingleRepository(
            ILogger<MailSingleRepository> logger,            
            IOptions<AppMongoClient.MongoClientSettings> mongoClientSettings
        ) {
            _logger = logger;
            _mongoClientSettings = mongoClientSettings.Value;            
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBaseProduction);
            _collection = mongoDatabase.GetCollection<MailSingleEntity>(_mongoClientSettings.MailSingleColletion);            
        }
        #endregion

        #region Methods Crud
        public async Task<ResponseDTO> InsertAsync(MailSingleEntity pEntity)
        {
            _logger.LogInformation("MailSingleRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtCreation = DateTime.Now;
                await _collection.InsertOneAsync(pEntity);
                _logger.LogInformation("MailSingleRepository.InsertAsync => OK");
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailSingleRepository.InsertAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("MailSingleRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(MailSingleEntity pEntity)
        {
            _logger.LogInformation("MailSingleRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<MailSingleEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
                MailSingleEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
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
                _logger.LogError($"MailSingleRepository.UpdateAsync => Exception: {ex.Message}");
            }
            _logger.LogInformation("MailSingleRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("MailSingleRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<MailSingleEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
                MailSingleEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                bool isSuccess = (entity != null) ? true : false;
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailSingleRepository.GetById => Exception: {ex.Message}");
            }
            _logger.LogInformation("MailSingleRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("MailSingleRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<MailSingleEntity, bool>> filter = x => x.IsActive == true;
                List<MailSingleEntity> listEntities = await _collection.Find(filter).ToListAsync();
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailSingleRepository.GetAll => Exception: {ex.Message}");
            }
            _logger.LogInformation("MailSingleRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
