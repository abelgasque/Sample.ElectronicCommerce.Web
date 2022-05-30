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
    public class MailMessageRepository
    {
        #region Variables
        private readonly ILogger<MailMessageRepository> _logger;

        private readonly MailBrokerDbContext _context;
        #endregion

        #region Constructor
        public MailMessageRepository(
            ILogger<MailMessageRepository> logger,
            MailBrokerDbContext context
        ) {
            _logger = logger;
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<ResponseDTO> InsertAsync(MailMessageEntity pEntity)
        {
            _logger.LogInformation("MailMessageRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                _context.MailMessage.Add(pEntity);
                int nuResult = await _context.SaveChangesAsync();
                bool isSuccess = (nuResult > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageRepository.InsertAsync => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageRepository.InsertAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailMessageRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(MailMessageEntity pEntity)
        {
            _logger.LogInformation("MailMessageRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                _context.MailMessage.Update(pEntity);
                int nuResult = await _context.SaveChangesAsync();
                bool isSuccess = (nuResult > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageRepository.UpdateAsync => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageRepository.UpdateAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailMessageRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(long pId)
        {
            _logger.LogInformation("MailMessageRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<MailMessageEntity> query = _context.MailMessage.AsNoTracking().Where(e => e.Id == pId);
                MailMessageEntity entity = await query.FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, entity);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageRepository.GetById => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageRepository.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailMessageRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll(bool? pIsActive)
        {
            _logger.LogInformation("MailMessageRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<MailMessageEntity> query = _context.MailMessage.AsNoTracking().Take(10000);
                query = (pIsActive != null) ? query.Where(e => e.IsActive == pIsActive.Value) : query;
                List<MailMessageEntity> listEntities = await query.OrderByDescending(e => e.Id).ToListAsync();
                string deMessage = (listEntities != null && listEntities.Count() > 0) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, listEntities);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageRepository.GetAll => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailMessageRepository.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailMessageRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
