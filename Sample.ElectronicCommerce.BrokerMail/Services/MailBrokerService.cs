using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping;
using Sample.ElectronicCommerce.BrokerMail.Repositories;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using Sample.ElectronicCommerce.Shared.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerMail.Services
{
    public class MailBrokerService
    {
        #region Variables
        private readonly ILogger<MailBrokerService> _logger;

        private readonly MailBrokerRepository _repository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public MailBrokerService(
            ILogger<MailBrokerService> logger, 
            MailBrokerRepository repository, 
            LogAppService logAppService
        ) {
            _logger = logger;
            _repository = repository;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods
        public async Task<ReturnDTO> InsertAsync(MailBrokerEntity pEntity)
        {
            _logger.LogInformation($"MailBrokerService.InsertAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                pEntity.DtCreation = DateTime.Now;
                responseDTO = await _repository.InsertAsync(pEntity);
                ReturnDTO returnDTO = await this.GetById(pEntity.Id);
                responseDTO = new ResponseDTO(returnDTO.IsSuccess, returnDTO.DeMessage, returnDTO.ResultObject);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailBrokerService.InsertAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "MailBrokerService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailBrokerService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(MailBrokerEntity pEntity)
        {
            _logger.LogInformation($"MailBrokerService.UpdateAsync => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetById(pEntity.Id);
                MailBrokerEntity entity = (MailBrokerEntity)responseDTO.DataObject;
                pEntity.Password = (string.IsNullOrEmpty(pEntity.Password)) ? entity.Password : pEntity.Password;
                pEntity.DtLastUpdate = DateTime.Now;
                responseDTO = await _repository.UpdateAsync(pEntity);
                ReturnDTO returnDTO = await this.GetById(pEntity.Id);
                responseDTO = new ResponseDTO(returnDTO.IsSuccess, returnDTO.DeMessage, returnDTO.ResultObject);
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailBrokerService.UpdateAsync => Exception: { ex.Message }");
            }
            await _logAppService.AppInsertAsync(0, "MailBrokerService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailBrokerService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(long pId)
        {
            _logger.LogInformation($"MailBrokerService.GetById => Start");
            ResponseDTO responseDTO;
            try
            {                
                responseDTO = await _repository.GetById(pId);
                if (responseDTO.IsSuccess && responseDTO.DataObject != null)
                {
                    MailBrokerEntity entity = (MailBrokerEntity)responseDTO.DataObject;
                    entity.Password = null;
                    responseDTO.DataObject = entity;
                }
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailBrokerService.GetById => Exception: { ex.Message }");
            }
            _logger.LogInformation($"MailBrokerService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll(bool? pIsActive)
        {
            _logger.LogInformation($"MailBrokerService.GetAll => Start");
            ResponseDTO responseDTO;
            try
            {
                responseDTO = await _repository.GetAll(pIsActive);
                List<MailBrokerEntity> listEntities = (List<MailBrokerEntity>)responseDTO.DataObject;
                foreach (MailBrokerEntity entity in listEntities)
                {
                    entity.Password = null;
                }
                responseDTO.DataObject = listEntities;
            }
            catch (Exception ex)
            {
                responseDTO = new ResponseDTO(false, AppConstant.StandardErrorMessageService, ex.Message.ToString(), ex.StackTrace.ToString(), null);
                _logger.LogError($"MailBrokerService.GetAll => Exception: { ex.Message }");
            }
            _logger.LogInformation($"MailBrokerService.GetAll => End");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
