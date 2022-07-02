using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.Settings;
using Sample.ElectronicCommerce.Core.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using System.Linq.Expressions;
using MongoDB.Bson;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;

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
            var mongoDatabase = mongoClient.GetDatabase(_mongoClientSettings.DataBase);
            _collection = mongoDatabase.GetCollection<LogAppEntity>(_mongoClientSettings.LogAppColletion);
        }
        #endregion

        #region Methods Crud
        public async Task<ResponseDTO> InsertAsync(LogAppEntity pEntity)
        {
            _logger.LogInformation("LogAppRepository.InsertAsync => Start");
            pEntity.DtCreation = DateTime.Now;
            await _collection.InsertOneAsync(pEntity);
            _logger.LogInformation("LogAppRepository.InsertAsync > Finish");
            return new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
        }

        public async Task<ResponseDTO> UpdateAsync(LogAppEntity pEntity)
        {
            _logger.LogInformation("LogAppRepository.UpdateAsync => Start");
            Expression<Func<LogAppEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
            LogAppEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            pEntity.DtLastUpdate = DateTime.Now;
            ResponseDTO responseDTO = new ResponseDTO()
            {
                IsSuccess = true,
                DeMessage = AppConstant.DeMessageSuccessWS,
                DataObject = await _collection.ReplaceOneAsync(filter, pEntity)
            };
            _logger.LogInformation("LogAppRepository.UpdateAsync => Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("LogAppRepository.GetById => Start");
            Expression<Func<LogAppEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
            LogAppEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, entity);
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