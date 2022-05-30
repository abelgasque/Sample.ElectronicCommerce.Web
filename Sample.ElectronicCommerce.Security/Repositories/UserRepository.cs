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
    public class UserRepository
    {
        #region Variables
        private readonly ILogger<UserRepository> _logger;

        private readonly SecurityDbContext _context;
        #endregion

        #region Constructor
        public UserRepository(
            ILogger<UserRepository> logger, 
            SecurityDbContext context
        ) {
            _logger = logger;
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<ResponseDTO> InsertAsync(UserEntity pEntity)
        {
            _logger.LogInformation("UserRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {                
                _context.User.Add(pEntity);
                int nuResult = await _context.SaveChangesAsync();
                bool isSuccess = (nuResult > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.InsertAsync => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.InsertAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(UserEntity pEntity)
        {
            _logger.LogInformation("UserRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                _context.User.Update(pEntity);
                int nuResult = await _context.SaveChangesAsync();
                bool isSuccess = (nuResult > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);                
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.UpdateAsync => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.UpdateAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(long pId)
        {
            _logger.LogInformation("UserRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<UserEntity> query = _context.User.AsNoTracking();
                query =  query.Where(e => e.Id == pId).Include(e => e.Roles);
                UserEntity entity = await query.FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, entity);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.GetById => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetByMail(string pMail)
        {
            _logger.LogInformation("UserRepository.GetByMail => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<UserEntity> query = _context.User.AsNoTracking();
                query = query.Where(u => u.Mail == pMail).Include(e => e.Roles);
                UserEntity entity = await query.FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, entity);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.GetByMail => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.GetByMail => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserRepository.GetByMail > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll(bool? pIsActive)
        {
            _logger.LogInformation("UserRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<UserEntity> query = _context.User.AsNoTracking().Take(10000);
                query = (pIsActive != null) ? query.Where(e => e.IsActive == pIsActive.Value) : query;
                List<UserEntity> listEntities = await query.OrderByDescending(e => e.Id).ToListAsync();
                string deMessage = (listEntities != null && listEntities.Count() > 0) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, listEntities);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.GetAll => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"UserRepository.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("UserRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
