﻿using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Mail.Repositories;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Services;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Mail.Services
{
    public class MailGroupService
    {
        #region Variables
        private readonly ILogger<MailGroupService> _logger;

        private readonly MailGroupRepository _repository;

        private readonly LogAppService _logAppService;
        #endregion

        #region Constructor
        public MailGroupService(
            ILogger<MailGroupService> logger,
            MailGroupRepository repository,
            LogAppService logAppService
        )
        {
            _logger = logger;
            _repository = repository;
            _logAppService = logAppService;
        }
        #endregion

        #region Methods  
        public async Task<ReturnDTO> InsertAsync(MailGroupEntity pEntity)
        {
            _logger.LogInformation($"MailGroupService.InsertAsync => Start");
            ResponseDTO responseDTO = await _repository.InsertAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "MailGroupService.InsertAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailGroupService.InsertAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> UpdateAsync(MailGroupEntity pEntity)
        {
            _logger.LogInformation($"MailGroupService.UpdateAsync => Start");
            ResponseDTO responseDTO = await _repository.UpdateAsync(pEntity);
            await _logAppService.AppInsertAsync(null, "MailGroupService.UpdateAsync", pEntity, responseDTO);
            _logger.LogInformation($"MailGroupService.UpdateAsync => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetById(string pId)
        {
            _logger.LogInformation($"MailGroupService.GetById => Start");
            ResponseDTO responseDTO = await _repository.GetById(pId);
            _logger.LogInformation($"MailGroupService.GetById => End");
            return new ReturnDTO(responseDTO);
        }

        public async Task<ReturnDTO> GetAll()
        {
            _logger.LogInformation("MailGroupService.GetAll => Start");
            ResponseDTO responseDTO = await _repository.GetAll();
            _logger.LogInformation("MailGroupService.GetAll > Finish");
            return new ReturnDTO(responseDTO);
        }
        #endregion
    }
}
