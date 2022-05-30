using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Sample.ElectronicCommerce.Core.Entities;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Repositories
{
    public class ApplicationRepository
    {
        #region Variables
        private readonly ILogger<ApplicationRepository> _logger;
        private readonly CoreSettings _coreSettings;
        
        private readonly IMongoCollection<ApplicationEntity> _collection;
        #endregion

        #region Constructor
        public ApplicationRepository(
            ILogger<ApplicationRepository> logger,
            IOptions<CoreSettings> coreSettings
        ) {
            _logger = logger;
            _coreSettings = coreSettings.Value;
            var mongoClient = new MongoClient(_coreSettings.MongoClient.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_coreSettings.MongoClient.DataBase);
            _collection = mongoDatabase.GetCollection<ApplicationEntity>(_coreSettings.ApplicationColletion);
        }
        #endregion

        #region Methods        
        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("ApplicationRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<ApplicationEntity, bool>> filter = x => x.IsActive.Equals(true);
                ICollection<ApplicationEntity> entity = await _collection.Find(filter).ToListAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                bool isSuccess = (entity != null) ? true : false;
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"ApplicationRepository.GetAll => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"ApplicationRepository.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("ApplicationRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
