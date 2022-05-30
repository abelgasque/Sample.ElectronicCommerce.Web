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
    public class MailBrokerRepository
    {
        #region Variables
        private readonly ILogger<MailBrokerRepository> _logger;

        private readonly MailBrokerDbContext _context;
        #endregion

        #region Constructor
        public MailBrokerRepository(
            ILogger<MailBrokerRepository> logger, 
            MailBrokerDbContext context
        ) {
            _logger = logger;
            _context = context;
        }
        #endregion

        #region Methods
        public async Task<ResponseDTO> InsertAsync(MailBrokerEntity pEntity)
        {
            _logger.LogInformation("MailBrokerRepository.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                _context.MailBroker.Add(pEntity);
                int nuResult = await _context.SaveChangesAsync();
                bool isSuccess = (nuResult > 0) ? true : false;
                string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                object dataObject = (isSuccess) ? pEntity : null;
                responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailBrokerRepository.InsertAsync => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailBrokerRepository.InsertAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailBrokerRepository.InsertAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> UpdateAsync(MailBrokerEntity pEntity)
        {
            _logger.LogInformation("MailBrokerRepository.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await this.GetById(pEntity.Id);
                if (responseDTO.IsSuccess && responseDTO.DataObject != null)
                {
                    if (string.IsNullOrEmpty(pEntity.Password))
                    {
                        MailBrokerEntity entity = (MailBrokerEntity)responseDTO.DataObject;
                        pEntity.Password = entity.Password;
                    }
                    _context.MailBroker.Update(pEntity);
                    int nuResult = await _context.SaveChangesAsync();
                    bool isSuccess = (nuResult > 0) ? true : false;
                    string deMessage = (isSuccess) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                    object dataObject = (isSuccess) ? pEntity : null;
                    responseDTO = new ResponseDTO(isSuccess, deMessage, dataObject);
                }
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailBrokerRepository.UpdateAsync => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailBrokerRepository.UpdateAsync => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailBrokerRepository.UpdateAsync > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetById(long pId)
        {
            _logger.LogInformation("MailBrokerRepository.GetById => Start");
            ResponseDTO responseDTO;
            try
            {
                IQueryable<MailBrokerEntity> query = _context.MailBroker.AsNoTracking().Where(e => e.Id == pId);
                MailBrokerEntity entity = await query.FirstOrDefaultAsync();
                string deMessage = (entity != null) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, entity);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailBrokerRepository.GetById => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailBrokerRepository.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailBrokerRepository.GetById > Finish");
            return responseDTO;
        }

        public async Task<ResponseDTO> GetAll(bool? pIsActive)
        {
            _logger.LogInformation("MailBrokerRepository.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {               
                IQueryable<MailBrokerEntity> query = _context.MailBroker.AsNoTracking().Take(10000);
                query = (pIsActive != null) ? query.Where(e => e.IsActive == pIsActive.Value) : query;
                List<MailBrokerEntity> listEntities = await query.OrderByDescending(e => e.Id).ToListAsync();
                string deMessage = (listEntities != null && listEntities.Count() > 0) ? AppConstant.DeMessageSuccessWS : AppConstant.DeMessageDataNotFoundWS;
                responseDTO = new ResponseDTO(true, deMessage, listEntities);
            }
            catch (SqlException ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageForDataBase, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailBrokerRepository.GetAll => SqlException: { ex.Message }");
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageRepository, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailBrokerRepository.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation("MailBrokerRepository.GetAll > Finish");
            return responseDTO;
        }
        #endregion
    }
}
