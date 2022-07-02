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
        )
        {
            _logger = logger;
            _mongoClientSettings = mongoClientSettings.Value;
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBase);
            _collection = mongoDatabase.GetCollection<MailGroupEntity>(_mongoClientSettings.MailGroupColletion);
        }
        #endregion

        #region Methods Crud
        public async Task<ResponseDTO> InsertAsync(MailGroupEntity pEntity)
        {
            _logger.LogInformation("MailGroupRepository.InsertAsync => Start");
            pEntity.DtCreation = DateTime.Now;
            await _collection.InsertOneAsync(pEntity);
            _logger.LogInformation("MailGroupRepository.InsertAsync => OK");
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            _logger.LogInformation("MailGroupRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(MailGroupEntity pEntity)
        {
            _logger.LogInformation("MailGroupRepository.UpdateAsync => Start");
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
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            _logger.LogInformation("MailGroupRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("MailGroupRepository.GetById => Start");
            Expression<Func<MailGroupEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
            MailGroupEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            bool isSuccess = (entity != null) ? true : false;
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            _logger.LogInformation("MailGroupRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("MailGroupRepository.GetAll => Start");
            Expression<Func<MailGroupEntity, bool>> filter = x => x.IsActive == true;
            List<MailGroupEntity> listEntities = await _collection.Find(filter).ToListAsync();
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            _logger.LogInformation("MailGroupRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
