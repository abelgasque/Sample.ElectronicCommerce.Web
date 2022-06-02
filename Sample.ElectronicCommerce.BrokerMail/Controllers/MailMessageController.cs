using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.BrokerMail.Entities;
using Sample.ElectronicCommerce.BrokerMail.Services;
using Sample.ElectronicCommerce.Core.Constants;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerMail.Controllers
{
    [ApiController]
    //[Authorize]
    [Route("[controller]")]
    public class MailMessageController : ControllerBase
    {
        #region Variables
        private readonly ILogger<MailMessageController> _logger;

        private readonly MailMessageService _service;
        #endregion

        #region Constructor
        public MailMessageController(
            ILogger<MailMessageController> logger, 
            MailMessageService service
        ) {   
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points
        /// POST: MailMessage/InsertAsync
        /// <summary>
        /// End Point que insere mensagem de e-mail
        /// </summary>        
        [HttpPost]
        [Route("InsertAsync")]
        public async Task<ActionResult<ReturnDTO>> InsertAsync([FromBody] MailMessageEntity pEntity)
        {
            _logger.LogInformation("MailMessageController.InsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.InsertAsync(pEntity);
                _logger.LogInformation($"MailMessageController.InsertAsync => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailMessageController.InsertAsync => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// PUT: MailMessage/UpdateAsync
        /// <summary>
        /// End Point que atualiza mensagem de e-mail
        /// </summary>
        [HttpPut]
        [Route("UpdateAsync")]
        public async Task<ActionResult<ReturnDTO>> UpdateAsync([FromBody] MailMessageEntity pEntity)
        {
            _logger.LogInformation("MailMessageController.UpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.UpdateAsync(pEntity);
                _logger.LogInformation($"MailMessageController.UpdateAsync => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailMessageController.UpdateAsync => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: MailMessage/GetById/pId
        /// <summary>
        /// End Point que busca mensagem de e-mail por codigo
        /// </summary>                
        [HttpGet]
        [Route("GetById/{pId}")]
        public async Task<ActionResult<ReturnDTO>> GetById(string pId)
        {
            _logger.LogInformation("MailMessageController.GetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetById(pId);
                _logger.LogInformation($"MailMessageController.GetById => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailMessageController.GetById => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: MailMessage/GetAll
        /// <summary>
        /// End Point que lista todas as mensagens de e-mails
        /// </summary>                
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ReturnDTO>> GetAll()
        {
            _logger.LogInformation("MailMessageController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetAll();
                _logger.LogInformation($"MailMessageController.GetAll => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailMessageController.GetAll => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion
    }
}