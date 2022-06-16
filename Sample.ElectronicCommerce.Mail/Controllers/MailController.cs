using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Mail.Services;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDb;
using Sample.ElectronicCommerce.Core.Util;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Mail.Controllers
{
    [ApiController]
    [Route("api/mail")]
    public class MailController : ControllerBase
    {
        #region Variables
        private readonly ILogger<MailController> _logger;

        private readonly MailBrokerService _mailBrokerService;

        private readonly MailGroupService _mailGroupService;

        private readonly MailSingleService _mailSingleService;
        #endregion

        #region Constructor
        public MailController(
            ILogger<MailController> logger,
            MailBrokerService mailBrokerService,
            MailGroupService mailGroupService,
            MailSingleService mailSingleService
        )
        {
            _logger = logger;
            _mailBrokerService = mailBrokerService;
            _mailGroupService = mailGroupService;
            _mailSingleService = mailSingleService;
        }
        #endregion

        #region End Points Mail Broker
        /// POST: api/mail/broker
        /// <summary>
        /// Ponto final que insere agente de email
        /// </summary>
        [HttpPost]
        [Route("broker")]
        public async Task<ActionResult<ReturnDTO>> MailBrokerInsertAsync([FromBody] MailBrokerEntity pEntity)
        {
            _logger.LogInformation("MailController.MailBrokerInsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("MailController.MailBrokerInsertAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _mailBrokerService.InsertAsync(pEntity);
                _logger.LogInformation($"MailController.MailBrokerInsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.MailBrokerInsertAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// PUT: api/mail/broker
        /// <summary>
        /// Ponto final que atualiza agente de email
        /// </summary>        
        [HttpPut]
        [Route("broker")]
        public async Task<ActionResult<ReturnDTO>> MailBrokerUpdateAsync([FromBody] MailBrokerEntity pEntity)
        {
            _logger.LogInformation("MailController.MailBrokerUpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("MailController.MailBrokerUpdateAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _mailBrokerService.UpdateAsync(pEntity);
                _logger.LogInformation($"MailController.MailBrokerUpdateAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.MailBrokerUpdateAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/mail/broker/{pId}
        /// <summary>
        /// Ponto final que busca agente de email por codigo
        /// </summary>        
        [HttpGet]
        [Route("broker/{pId}")]
        public async Task<ActionResult<ReturnDTO>> MailBrokerGetById(string pId)
        {
            _logger.LogInformation("MailController.MailBrokerGetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _mailBrokerService.GetById(pId);
                _logger.LogInformation($"MailController.MailBrokerGetById => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.MailBrokerGetById => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/mail/broker
        /// <summary>
        /// Ponto final que lista todos os agentes de email
        /// </summary>        
        [HttpGet]
        [Route("broker")]
        public async Task<ActionResult<ReturnDTO>> MailBrokerGetAll()
        {
            _logger.LogInformation("MailController.MailBrokerGetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _mailBrokerService.GetAll();
                _logger.LogInformation($"MailController.MailBrokerGetAll => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.MailBrokerGetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }
        #endregion

        #region End Points Mail Group
        /// POST: api/mail/group
        /// <summary>
        /// Ponto final que insere grupo de email
        /// </summary>
        [HttpPost]
        [Route("group")]
        public async Task<ActionResult<ReturnDTO>> MailGroupInsertAsync([FromBody] MailGroupEntity pEntity)
        {
            _logger.LogInformation("MailController.MailGroupInsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("MailController.MailGroupInsertAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _mailGroupService.InsertAsync(pEntity);
                _logger.LogInformation($"MailController.MailGroupInsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.MailGroupInsertAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// PUT: api/mail/group
        /// <summary>
        /// Ponto final que atualiza grupo de email
        /// </summary>        
        [HttpPut]
        [Route("group")]
        public async Task<ActionResult<ReturnDTO>> MailGroupUpdateAsync([FromBody] MailGroupEntity pEntity)
        {
            _logger.LogInformation("MailController.MailGroupUpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("MailController.MailGroupUpdateAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _mailGroupService.UpdateAsync(pEntity);
                _logger.LogInformation($"MailController.MailGroupUpdateAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.UpdateAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/mail/group/pId
        /// <summary>
        /// Ponto final que busca grupo de email por codigo
        /// </summary>        
        [HttpGet]
        [Route("group/{pId}")]
        public async Task<ActionResult<ReturnDTO>> MailGroupGetById(string pId)
        {
            _logger.LogInformation("MailController.MailGroupGetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _mailGroupService.GetById(pId);
                _logger.LogInformation($"MailController.MailGroupGetById => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.MailGroupGetById => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/mail/group
        /// <summary>
        /// Ponto final de lista groupo de email
        /// </summary>        
        [HttpGet]
        [Route("group")]
        public async Task<ActionResult<ReturnDTO>> MailGroupGetAll()
        {
            _logger.LogInformation("MailController.MailGroupGetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _mailGroupService.GetAll();
                _logger.LogInformation($"MailController.MailGroupGetAll => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.MailGroupGetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }
        #endregion

        #region End Points Mail Single
        /// POST: api/mail/single
        /// <summary>
        /// Ponto final que insere email
        /// </summary>            
        [HttpPost]
        [Route("single")]
        public async Task<ActionResult<ReturnDTO>> MailSingleInsertAsync([FromBody] MailSingleEntity pEntity)
        {
            _logger.LogInformation("MailController.MailSingleInsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _mailSingleService.InsertAsync(pEntity);
                _logger.LogInformation($"MailController.MailSingleInsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.MailSingleInsertAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// PUT: api/mail/single
        /// <summary>
        /// Ponto final que atualiza email
        /// </summary>                
        [HttpPut]
        [Route("single")]
        public async Task<ActionResult<ReturnDTO>> MailSingleUpdateAsync([FromBody] MailSingleEntity pEntity)
        {
            _logger.LogInformation("MailController.MailSingleUpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _mailSingleService.UpdateAsync(pEntity);
                _logger.LogInformation($"MailController.MailSingleUpdateAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.MailSingleUpdateAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: api/mail/single/pId
        /// <summary>
        /// Ponto final que busca email por codigo
        /// </summary>
        [HttpGet]
        [Route("single/{pId}")]
        public async Task<ActionResult<ReturnDTO>> MailSingleGetById(string pId)
        {
            _logger.LogInformation("MailController.MailSingleGetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _mailSingleService.GetById(pId);
                _logger.LogInformation($"MailController.MailSingleGetById => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.MailSingleGetById => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: api/mail/single
        /// <summary>
        /// Ponto final com lista de todos os emails
        /// </summary>                
        [HttpGet]
        [Route("single")]
        public async Task<ActionResult<ReturnDTO>> MailSingleGetAll()
        {
            _logger.LogInformation("MailController.MailSingleGetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _mailSingleService.GetAll();
                _logger.LogInformation($"MailController.MailSingleGetAll => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.MailSingleGetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion
    }
}