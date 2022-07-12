using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Util;

namespace Sample.ElectronicCommerce.Core.Repositories
{
    public class MailMessageRepository
    {
        #region Variables
        private readonly ILogger<MailMessageRepository> _logger;

        private readonly AppMongoClient.MongoClientSettings _mongoClientSettings;

        private readonly IMongoCollection<MailSingleEntity> _collection;
        #endregion

        #region Constructor
        public MailMessageRepository(
            ILogger<MailMessageRepository> logger,
            IOptions<AppMongoClient.MongoClientSettings> mongoClientSettings
        )
        {
            _logger = logger;
            _mongoClientSettings = mongoClientSettings.Value;
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBase);
            _collection = mongoDatabase.GetCollection<MailSingleEntity>(_mongoClientSettings.MailSingleColletion);
        }
        #endregion

        #region Methods Crud
        public async Task<ResponseDTO> InsertAsync(MailSingleEntity pEntity)
        {
            _logger.LogInformation("MailMessageRepository.InsertAsync => Start");
            pEntity.DtCreation = DateTime.Now;
            await _collection.InsertOneAsync(pEntity);
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            _logger.LogInformation("MailMessageRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(MailSingleEntity pEntity)
        {
            _logger.LogInformation("MailMessageRepository.UpdateAsync => Start");
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
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            _logger.LogInformation("MailMessageRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("MailMessageRepository.GetById => Start");
            Expression<Func<MailSingleEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
            MailSingleEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            bool isSuccess = (entity != null) ? true : false;
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            _logger.LogInformation("MailMessageRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("MailMessageRepository.GetAll => Start");
            Expression<Func<MailSingleEntity, bool>> filter = x => x.IsActive == true;
            List<MailSingleEntity> listEntities = await _collection.Find(filter).ToListAsync();
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            _logger.LogInformation("MailMessageRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
