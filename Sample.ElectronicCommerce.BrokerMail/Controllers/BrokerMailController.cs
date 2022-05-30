using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.BrokerMail.Entities;
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
    public class BrokerMailController : ControllerBase
    {
        #region Variables
        private readonly ILogger<BrokerMailController> _logger;

        private readonly BrokerMailService _service;
        #endregion

        #region Constructor
        public BrokerMailController(
            ILogger<BrokerMailController> logger, 
            BrokerMailService service
        ) {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points
        /// POST: BrokerMail/InsertAsync
        /// <summary>
        /// End Point que insere agente de e-mail
        /// </summary>                
        [HttpPost]
        [Route("InsertAsync")]
        public async Task<ActionResult<ReturnDTO>> InsertAsync([FromBody] BrokerMailEntity pEntity)
        {
            _logger.LogInformation("BrokerMailController.InsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.InsertAsync(pEntity);
                _logger.LogInformation($"BrokerMailController.InsertAsync => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrokerMailController.InsertAsync => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// PUT: BrokerMail/UpdateAsync
        /// <summary>
        /// End Point que atualiza agente de e-mail
        /// </summary>                
        [HttpPut]
        [Route("UpdateAsync")]
        public async Task<ActionResult<ReturnDTO>> UpdateAsync([FromBody] BrokerMailEntity pEntity)
        {
            _logger.LogInformation("BrokerMailController.UpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.UpdateAsync(pEntity);
                _logger.LogInformation($"BrokerMailController.UpdateAsync => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrokerMailController.UpdateAsync => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: BrokerMail/GetById/pId
        /// <summary>
        /// End Point que busca agente de e-mail por codigo
        /// </summary>                
        [HttpGet]
        [Route("GetById/{pId}")]
        public async Task<ActionResult<ReturnDTO>> GetById(string pId)
        {
            _logger.LogInformation("BrokerMailController.GetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetById(pId);
                _logger.LogInformation($"BrokerMailController.GetById => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrokerMailController.GetById => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: BrokerMail/GetAll
        /// <summary>
        /// End Point que lista todos os agentes de e-mails
        /// </summary>
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ReturnDTO>> GetAll()
        { 
            _logger.LogInformation("BrokerMailController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetAll();
                _logger.LogInformation($"BrokerMailController.GetAll => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrokerMailController.GetAll => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion
    }
}