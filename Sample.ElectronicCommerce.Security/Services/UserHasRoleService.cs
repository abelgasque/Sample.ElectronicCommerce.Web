using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Security.Repositories;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using Sample.ElectronicCommerce.Shared.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Services
{
    public class UserHasRoleService
    {
        #region Variables
        private readonly ILogger<UserHasRoleService> _logger;

        private readonly UserHasRoleRepository _repository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public UserHasRoleService(
            ILogger<UserHasRoleService> logger, 
            UserHasRoleRepository repository, 
            LogAppService logAppService
        ) {
            _logger = logger;
            _repository = repository;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods    
        public async Task<ReturnDTO> PersistAsync(long pIdUser, List<UserRoleEntity> pNewListEntities)
        {
            ReturnDTO returnDTO = await this.GetByIdUser(pIdUser);
            List<UserHasRoleEntity> listUserHasRole = (List<UserHasRoleEntity>)returnDTO.ResultObject;
            if (listUserHasRole != null && listUserHasRole.Count > 0)
            {
                foreach (UserRoleEntity entity in pNewListEntities)
                {
                    bool isExists = listUserHasRole.Exists(e => e.IdUserRole == entity.Id);
                    if (!isExists)
                    {
                        UserHasRoleEntity userHasRole = new UserHasRoleEntity()
                        {
                            DtCreation = DateTime.Now,
                            IdUser = pIdUser,
                            IdUserRole = entity.Id
                        };
                        returnDTO = await this.InsertAsync(userHasRole);
                    }
                }

                foreach (UserHasRoleEntity entity in listUserHasRole)
                {
                    bool isExists = pNewListEntities.Exists(e => e.Id == entity.IdUserRole);
                    if (!isExists)
                    {
                        returnDTO = await this.DeleteAsync(pIdUser, entity.IdUserRole);
                    }
                }
            }
            else
            {
                foreach (UserRoleEntity entity in pNewListEntities)
                {
                    UserHasRoleEntity userHasRole = new UserHasRoleEntity()
                    {
                        DtCreation = DateTime.Now,
                        IdUser = pIdUser,
                        IdUserRole = entity.Id
                    };
                    returnDTO = await this.InsertAsync(userHasRole);
                }
            }

            return returnDTO;
        }
        
        private async Task<ReturnDTO> InsertAsync(UserHasRoleEntity pEntity)
        {
            _logger.LogInformation($"UserHasRoleService.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtCreation = DateTime.Now;
                responseDTO = await _repository.InsertAsync(pEntity);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserHasRoleService.InsertAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "UserHasRoleService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"UserHasRoleService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        private async Task<ReturnDTO> DeleteAsync(long pIdUser, long pIdUserRole)
        {
            _logger.LogInformation($"UserHasRoleService.DeleteAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.DeleteAsync(pIdUser, pIdUserRole);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserHasRoleService.DeleteAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "UserHasRoleService.DeleteAsync", pIdUser, responseDTO);
            _logger.LogInformation($"UserHasRoleService.DeleteAsync => End");
            return new ReturnDTO(responseDTO);
        }

        private async Task<ReturnDTO> GetByIdUser(long pIdUser)
        {
            _logger.LogInformation($"UserHasRoleService.GetByIdUser => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetByIdUser(pIdUser);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserHasRoleService.GetByIdUser => Exception: { ex.Message }");
            }
            _logger.LogInformation($"UserHasRoleService.GetByIdUser => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
