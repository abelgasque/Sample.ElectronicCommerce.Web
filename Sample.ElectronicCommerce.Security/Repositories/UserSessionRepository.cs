using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Entities;
using Sample.ElectronicCommerce.Security.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using Sample.ElectronicCommerce.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Repositories
{
    public class UserSessionRepository
    {
        #region Variables
        private readonly ILogger<UserSessionRepository> _logger;

        private readonly SecurityDbContext _context;

        private readonly DataBaseHelper _dbHelper;
        #endregion

        #region Constructor
        public UserSessionRepository(
            ILogger<UserSessionRepository> logger, 
            SecurityDbContext context, 
            DataBaseHelper dbHelper
        ) {
            _logger = logger;
            _context = context;
            _dbHelper = dbHelper;
        }
        #endregion

        #region Methods
        public async Task<ResponseDTO> InsertAsync(UserSessionEntity pEntity)
        {
            _logger.LogInformation("UserSessionRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                _context.UserSession.Add(pEntity);
                int nuResult = await _context.SaveChangesAsync();
                bool isSuccess = (nuResult > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.InsertAsync => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.InsertAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserSessionRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(UserSessionEntity pEntity)
        {
            _logger.LogInformation("UserSessionRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                _context.UserSession.Update(pEntity);
                int nuResult = await _context.SaveChangesAsync();
                bool isSuccess = (nuResult > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.UpdateAsync => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.UpdateAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserSessionRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(long pId)
        {
            _logger.LogInformation("UserSessionRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<UserSessionEntity> query = _context.UserSession.AsNoTracking();                
                query = query.Where(e => e.Id == pId);
                UserSessionEntity entity = await query.FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                bool isSuccess = (entity != null) ? true : false;
                responseDTO = new ResponseDTO(isSuccess, deMessage, entity);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.GetById => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserSessionRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll(bool? pIsActive)
        {
            _logger.LogInformation("UserSessionRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<UserSessionEntity> query = _context.UserSession.AsNoTracking().Take(10000);
                query = (pIsActive != null) ? query.Where(e => e.IsActive == pIsActive.Value) : query;
                List <UserSessionEntity> listEntities = await query.OrderByDescending(e => e.Id).ToListAsync();                
                string deMessage = (listEntities != null && listEntities.Count() > 0) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;                
                responseDTO = new ResponseDTO(true, deMessage, listEntities);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.GetAll => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserSessionRepository.GetAll > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetByIdUserWithAuthFailed(long pIdUser)
        {
            _logger.LogInformation("UserSessionRepository.GetByIdUserWithAuthFailed => Start");
            ResponseDTO responseDTO;
            try
            {                
                IQueryable<UserSessionEntity> query = _context.UserSession.AsNoTracking();
                query = query.Where(e => e.IdUser == pIdUser).Where(e => e.IsSuccess == false);
                query = query.Where(e => e.DtCreation >= DateTime.Now.AddMinutes(-5) && e.DtCreation <= DateTime.Now);
                UserSessionEntity entity = await query.FirstOrDefaultAsync();
                bool isSuccess = (entity != null) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? entity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.GetByIdUserWithAuthFailed => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserSessionRepository.GetByIdUserWithAuthFailed => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserSessionRepository.GetByIdUserWithAuthFailed > Finish");
            return responseDTO;
        }
        #endregion
    }
}
