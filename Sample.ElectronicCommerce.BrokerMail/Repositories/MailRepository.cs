using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.BrokerMail.Entities.EF;
using Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerMail.Repositories
{
    public class MailRepository
    {
        #region Variables
        private readonly ILogger<MailRepository> _logger;

        private readonly MailBrokerDbContext _context;
        #endregion

        #region Constructor
        public MailRepository(
            ILogger<MailRepository> logger,
            MailBrokerDbContext context
        ) {
            _logger = logger;
            _context = context;
        }
        #endregion

        #region Methods

        public async Task<ResponseDTO> InsertAsync(MailEntity pEntity)
        {
            _logger.LogInformation("MailRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                _context.Mail.Add(pEntity);
                int nuResult = await _context.SaveChangesAsync();
                bool isSuccess = (nuResult > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailRepository.InsertAsync => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailRepository.InsertAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(MailEntity pEntity)
        {
            _logger.LogInformation("MailRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                _context.Mail.Update(pEntity);
                int nuResult = await _context.SaveChangesAsync();
                bool isSuccess = (nuResult > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailRepository.UpdateAsync => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailRepository.UpdateAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(long pId)
        {
            _logger.LogInformation("MailRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<MailEntity> query = _context.Mail.AsNoTracking().Where(e => e.Id == pId);
                MailEntity entity = await query.FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, entity);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailRepository.GetById => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailRepository.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll(bool? pIsActive)
        {
            _logger.LogInformation("MailRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<MailEntity> query = _context.Mail.AsNoTracking().Take(10000);
                query = (pIsActive != null) ? query.Where(e => e.IsActive == pIsActive.Value) : query;
                List<MailEntity> listEntities = await query.OrderByDescending(e => e.Id).ToListAsync();
                string deMessage = (listEntities != null && listEntities.Count() > 0) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, listEntities);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailRepository.GetAll => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailRepository.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
