using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Sample.ElectronicCommerce.Shared.Entities.Settings;
using MongoDB.Driver;
using Sample.ElectronicCommerce.Security.Entities;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;

namespace Sample.ElectronicCommerce.Security.Repositories
{
    public class UserRoleRepository
    {
        #region Variables
        private readonly ILogger<UserRoleRepository> _logger;
        
        private readonly SecuritySettings _securitySettings;
        
        private readonly IMongoCollection<UserRoleEntity> _collection;
        #endregion

        #region Constructor
        public UserRoleRepository(
            ILogger<UserRoleRepository> logger,
            IOptions<SecuritySettings> securitySettings           
        ) {
            _logger = logger;
            _securitySettings = securitySettings.Value;
            var mongoClient = new MongoClient(_securitySettings.MongoClient.GetConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(_securitySettings.MongoClient.DataBase);
            _collection = mongoDatabase.GetCollection<UserRoleEntity>(_securitySettings.UserRoleColletion);
        }
        #endregion

        #region Methods
        public async Task<ResponseDTO> GetAll()
        {
            _logger.LogInformation("UserRoleRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                Expression<Func<UserRoleEntity, bool>> filter = x => x.IsActive.Equals(true);
                List<UserRoleEntity> listEntities = await _collection.Find(filter).ToListAsync();                
                responseDTO = new ResponseDTO(true, AppConstant.DeMessageSuccessWS, listEntities);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRoleRepository.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserRoleRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
