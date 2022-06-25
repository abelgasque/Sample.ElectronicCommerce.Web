using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Core.Entities.DataBase.Mapping;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq.Expressions;
using MongoDB.Bson;

namespace Sample.ElectronicCommerce.Core.Repositories
{
    public class LogAppRepository
    {
        #region Variables
        private readonly ILogger<LogAppRepository> _logger;

        private readonly AppSettings _appSettings;

        private readonly AppMongoClient.MongoClientSettings _mongoClientSettings;

        private readonly IMongoCollection<LogAppEntity> _collection;
        #endregion

        #region Constructor
        public LogAppRepository(
            ILogger<LogAppRepository> logger,
            IOptions<AppSettings> appSettings,
            IOptions<AppMongoClient.MongoClientSettings> mongoClientSettings
        )
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _mongoClientSettings = mongoClientSettings.Value;
            var mongoClient = new MongoClient(_mongoClientSettings.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBaseProduction);
            _collection = mongoDatabase.GetCollection<LogAppEntity>(_mongoClientSettings.LogAppColletion);
        }
        #endregion

        #region Methods
        public async Task<ResponseDTO> InsertAsync(LogAppEntity pEntity)
        {
            _logger.LogInformation("LogAppRepository.InsertAsync => Start");
            pEntity.DtCreation = DateTime.Now;
            await _collection.InsertOneAsync(pEntity);
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
            _logger.LogInformation("LogAppRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(LogAppEntity pEntity)
        {
            _logger.LogInformation("LogAppRepository.UpdateAsync => Start");
            Expression<Func<LogAppEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
            LogAppEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            bool isSuccess = (entity != null) ? true : false;
            string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            object dataObject = (isSuccess) ? pEntity : null;
            if (isSuccess)
            {
                pEntity.DtLastUpdate = DateTime.Now;
                await _collection.ReplaceOneAsync(filter, pEntity);
            }
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            _logger.LogInformation("LogAppRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("LogAppRepository.GetById => Start");
            Expression<Func<LogAppEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
            LogAppEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
            bool isSuccess = (entity != null) ? true : false;
            ResponseDTO responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            _logger.LogInformation("LogAppRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("LogAppRepository.GetAll => Start");
            Expression<Func<LogAppEntity, bool>> filter = x => x.IsActive == true;
            List<LogAppEntity> listEntities = await _collection.Find(filter).ToListAsync();
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            _logger.LogInformation("LogAppRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}