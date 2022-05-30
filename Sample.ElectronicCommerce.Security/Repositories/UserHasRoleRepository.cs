using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Entities;
using Sample.ElectronicCommerce.Security.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Repositories
{
    public class UserHasRoleRepository
    {
        #region Variables
        private readonly ILogger<UserHasRoleRepository> _logger;

        private readonly SecurityDbContext _context;
        #endregion

        #region Constructor
        public UserHasRoleRepository(
            ILogger<UserHasRoleRepository> logger, 
            SecurityDbContext context
        ) {
            _logger = logger;
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<ResponseDTO> InsertAsync(UserHasRoleEntity pEntity)
        {
            _logger.LogInformation("UserHasRoleRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                _context.UserHasRole.Add(pEntity);
                int nuResult = await _context.SaveChangesAsync();
                bool isSuccess = (nuResult > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserHasRoleRepository.InsertAsync => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserHasRoleRepository.InsertAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserHasRoleRepository.InsertAsync > Finish");
            return responseDTO;
        }        

        public async Task<ResponseDTO> DeleteAsync(long pIdUser, long pIdUserRole)
        {
            _logger.LogInformation("UserHasRoleRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<UserHasRoleEntity> query = _context.UserHasRole.AsNoTracking();
                query = query.Where(e => e.IdUser == pIdUser).Where(e => e.IdUserRole == pIdUserRole);
                UserHasRoleEntity entity = await query.FirstOrDefaultAsync();
                if (entity != null)
                {
                    _context.UserHasRole.Remove(entity);
                    int nuResult = await _context.SaveChangesAsync();
                    bool isSuccess = (nuResult > 0) ? true : false;
                    string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                    object dataObject = (isSuccess) ? entity : null;
                    responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
                }
                else
                {
                    responseDTO = new ResponseDTO(false, AppConstant.DeMessageDataNotFoundWS, null);
                }
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserHasRoleRepository.UpdateAsync => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserHasRoleRepository.UpdateAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserHasRoleRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetByIdUser(long pIdUser)
        {
            _logger.LogInformation("UserHasRoleRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<UserHasRoleEntity> query = _context.UserHasRole.AsNoTracking().Where(e => e.IdUser == pIdUser);                
                List<UserHasRoleEntity> listEntities = await query.ToListAsync();
                bool isSuccess = (listEntities != null && listEntities.Count > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? listEntities : new List<UserHasRoleEntity>();
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserHasRoleRepository.GetById => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserHasRoleRepository.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserHasRoleRepository.GetById > Finish");
            return responseDTO;
        }
        #endregion
    }
}
