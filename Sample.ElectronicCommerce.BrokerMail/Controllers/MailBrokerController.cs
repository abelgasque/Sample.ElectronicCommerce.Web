using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.BrokerMail.Entities.EF.Mapping;
using Sample.ElectronicCommerce.BrokerMail.Services;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerMail.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = UserRoleConstant.CodeAdmin)]
    [Route("[controller]")]
    public class MailBrokerController : ControllerBase
    {
        #region Variables
        private readonly ILogger<MailBrokerController> _logger;

        private readonly MailBrokerService _service;
        #endregion

        #region Constructor
        public MailBrokerController(
            ILogger<MailBrokerController> logger, 
            MailBrokerService service
        ) {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points
        /// POST: MailBroker/InsertAsync
        /// <summary>
        /// End Point que insere agente de e-mail
        /// </summary>                
        [HttpPost]
        [Route("InsertAsync")]
        public async Task<ActionResult<ReturnDTO>> InsertAsync([FromBody] MailBrokerEntity pEntity)
        {
            _logger.LogInformation("MailBrokerController.InsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.InsertAsync(pEntity);
                _logger.LogInformation($"MailBrokerController.InsertAsync => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailBrokerController.InsertAsync => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// PUT: MailBroker/UpdateAsync
        /// <summary>
        /// End Point que atualiza agente de e-mail
        /// </summary>                
        [HttpPut]
        [Route("UpdateAsync")]
        public async Task<ActionResult<ReturnDTO>> UpdateAsync([FromBody] MailBrokerEntity pEntity)
        {
            _logger.LogInformation("MailBrokerController.UpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.UpdateAsync(pEntity);
                _logger.LogInformation($"MailBrokerController.UpdateAsync => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailBrokerController.UpdateAsync => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: MailBroker/GetById/pId
        /// <summary>
        /// End Point que busca agente de e-mail por codigo
        /// </summary>                
        [HttpGet]
        [Route("GetById/{pId}")]
        public async Task<ActionResult<ReturnDTO>> GetById(long pId)
        {
            _logger.LogInformation("MailBrokerController.GetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetById(pId);
                _logger.LogInformation($"MailBrokerController.GetById => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailBrokerController.GetById => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: MailBroker/GetAll
        /// <summary>
        /// End Point que lista todos os agentes de e-mails
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ReturnDTO>> GetAll([FromQuery] bool? pIsActive)
        { 
            _logger.LogInformation("MailBrokerController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetAll(pIsActive);
                _logger.LogInformation($"MailBrokerController.GetAll => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailBrokerController.GetAll => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion
    }
}