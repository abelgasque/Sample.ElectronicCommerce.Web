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
    public class MailController : ControllerBase
    {
        #region Variables
        private readonly ILogger<MailController> _logger;

        private readonly MailService _service;
        #endregion

        #region Constructor
        public MailController(
            ILogger<MailController> logger, 
            MailService service
        ) {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points
        /// POST: Mail/InsertAsync
        /// <summary>
        /// End Point que insere e-mail
        /// </summary>            
        [HttpPost]
        [Route("InsertAsync")]
        public async Task<ActionResult<ReturnDTO>> InsertAsync([FromBody] MailEntity pEntity)
        {
            _logger.LogInformation("MailController.InsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.InsertAsync(pEntity);
                _logger.LogInformation($"MailController.InsertAsync => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.InsertAsync => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// PUT: Mail/UpdateAsync
        /// <summary>
        /// End Point que atualiza e-mail
        /// </summary>                
        [HttpPut]
        [Route("UpdateAsync")]
        public async Task<ActionResult<ReturnDTO>> UpdateAsync([FromBody] MailEntity pEntity)
        {
            _logger.LogInformation("MailController.UpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.UpdateAsync(pEntity);
                _logger.LogInformation($"MailController.UpdateAsync => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.UpdateAsync => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: Mail/GetById/pId
        /// <summary>
        /// End Point que busca e-mail por codigo
        /// </summary>
        [HttpGet]
        [Route("GetById/{pId}")]
        public async Task<ActionResult<ReturnDTO>> GetById(string pId)
        {
            _logger.LogInformation("MailController.GetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetById(pId);
                _logger.LogInformation($"MailController.GetById => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.GetById => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: Mail/GetAll
        /// <summary>
        /// End Point que lista todos os e-mails
        /// </summary>                
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ReturnDTO>> GetAll()
        {
            _logger.LogInformation("MailController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetAll();
                _logger.LogInformation($"MailController.GetAll => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"MailController.GetAll => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion
    }
}