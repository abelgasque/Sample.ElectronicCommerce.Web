using AppMongoClient = Sample.ElectronicCommerce.Core.Entities.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Sample.ElectronicCommerce.Core.Util;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;

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
        )
        {
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
            pEntity.DtCreation = DateTime.Now;
            await _collection.InsertOneAsync(pEntity);
            _logger.LogInformation("OrganizationRepository.InsertAsync => Finish");
            return new ResponseDTO(true, AppConstant.DeMessageSuccessWS, pEntity);
        }

        public async Task<ResponseDTO> UpdateAsync(OrganizationEntity pEntity)
        {
            _logger.LogInformation("OrganizationRepository.UpdateAsync => Start");
            Expression<Func<OrganizationEntity, bool>> filter = x => x.Id.Equals(pEntity.Id);
            OrganizationEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            pEntity.DtLastUpdate = DateTime.Now;
            ResponseDTO responseDTO = new ResponseDTO()
            {
                IsSuccess = true,
                DeMessage = AppConstant.DeMessageSuccessWS,
                DataObject = await _collection.ReplaceOneAsync(filter, pEntity)
            };
            _logger.LogInformation("OrganizationRepository.UpdateAsync => Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(string pId)
        {
            _logger.LogInformation("OrganizationRepository.GetById => Start");
            Expression<Func<OrganizationEntity, bool>> filter = x => x.Id.Equals(ObjectId.Parse(pId));
            OrganizationEntity entity = await _collection.Find(filter).FirstOrDefaultAsync();
            if (entity == null) throw new BadRequestException(AppConstant.DeMessageDataNotFoundWS);
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, entity);
            _logger.LogInformation("OrganizationRepository.GetById => Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("OrganizationRepository.GetAll => Start");
            Expression<Func<OrganizationEntity, bool>> filter = x => x.IsActive == true;
            List<OrganizationEntity> listEntities = await _collection.Find(filter).ToListAsync();
            ResponseDTO responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            _logger.LogInformation("OrganizationRepository.GetAll => Finish");
            return responseDTO;
        }
        #endregion
    }
}