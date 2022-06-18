using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Entities.DataBase.Mapping;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Services;
using Sample.ElectronicCommerce.Core.Util;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Controllers
{
    [ApiController]    
    [Route("api/core")]
    public class CoreController : ControllerBase
    {
        #region Variables
        private readonly ILogger<CoreController> _logger;

        private readonly LogAppService _logAppService;

        private readonly OrganizationService _organizationService;
        #endregion

        #region Constructor
        public CoreController(
            ILogger<CoreController> logger,
            LogAppService service,
            OrganizationService organizationService
            )
        {
            _logger = logger;
            _logAppService = service;
            _organizationService = organizationService;
        }
        #endregion

        #region End Points Log App
        /// POST: log-app
        /// <summary>
        /// Ponto final que insere historico aplicacao
        /// </summary>
        //[Authorize(Roles = UserRoleConstant.CodeAdmin)]
        [HttpPost]
        [Route("log-app")]
        public async Task<ActionResult<ReturnDTO>> LogAppInsertAsync([FromBody] LogAppEntity pEntity)
        {
            _logger.LogInformation("CoreController.LogAppInsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _logAppService.InsertAsync(pEntity);
                _logger.LogInformation($"CoreController.LogAppInsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CoreController.LogAppInsertAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// PUT: log-app
        /// <summary>
        /// Ponto final que atualiza historico aplicacao
        /// </summary>     
        [HttpPut]
        [Route("log-app")]
        public async Task<ActionResult<ReturnDTO>> LogAppUpdateAsync([FromBody] LogAppEntity pEntity)
        {
            _logger.LogInformation("CoreController.LogAppUpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _logAppService.UpdateAsync(pEntity);
                _logger.LogInformation($"CoreController.LogAppUpdateAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CoreController.LogAppUpdateAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: log-app/{pId}
        /// <summary>
        /// Ponto final que busca historico aplicacao por codigo
        /// </summary>
        [Route("log-app/{pId}")]
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> LogAppGetById(long pId)
        {
            _logger.LogInformation("CoreController.LogAppGetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _logAppService.GetById(pId);
                _logger.LogInformation($"CoreController.LogAppGetById => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CoreController.LogAppGetById => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: log-app
        /// <summary>
        /// Ponto final que lista todos os historicos aplicacao
        /// </summary>
        [Route("log-app")]
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> LogAppGetAll([FromQuery] bool? pIsActive)
        {
            _logger.LogInformation("CoreController.LogAppGetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _logAppService.GetAll(pIsActive);
                _logger.LogInformation($"CoreController.LogAppGetAll => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CoreController.LogAppGetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: log-app/GetLogAppForChartDynamic
        /// <summary>
        /// Ponto final que exibe grafico de linha mensal do historico do sistema
        /// </summary>
        [Route("log-app/GetLogAppForChartDynamic")]
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> LogAppGetLogAppForChartMonth([FromQuery] bool pMustFilterYear)
        {
            _logger.LogInformation("CoreController.LogAppGetLogAppForChartMonth => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _logAppService.GetLogAppForChartDynamic(pMustFilterYear);
                _logger.LogInformation($"CoreController.LogAppGetLogAppForChartMonth => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CoreController.LogAppGetLogAppForChartMonth => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: log-app/GetLogAppDay
        /// <summary>
        /// Ponto final consulta de logs diarios do sistema
        /// </summary>
        [Route("log-app/GetLogAppDay")]
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> LogAppGetLogAppDay()
        {
            _logger.LogInformation("CoreController.LogAppGetLogAppDay => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _logAppService.GetLogAppDay();
                _logger.LogInformation($"CoreController.LogAppGetLogAppDay => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CoreController.LogAppGetLogAppDay => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion

        #region End Points Organization
        /// POST: api/core/organization
        /// <summary>
        /// Ponto final que insere organização
        /// </summary>
        [HttpPost]
        [Route("organization")]
        public async Task<ActionResult<ReturnDTO>> OrganizationInsertAsync([FromBody] OrganizationEntity pEntity)
        {
            _logger.LogInformation("CoreController.OrganizationInsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("CoreController.OrganizationInsertAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _organizationService.InsertAsync(pEntity);
                _logger.LogInformation($"CoreController.OrganizationInsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CoreController.OrganizationInsertAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// PUT: api/core/organization
        /// <summary>
        /// Ponto final que atualiza organização
        /// </summary>        
        [HttpPut]
        [Route("organization")]
        public async Task<ActionResult<ReturnDTO>> OrganizationUpdateAsync([FromBody] OrganizationEntity pEntity)
        {
            _logger.LogInformation("CoreController.OrganizationUpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("CoreController.OrganizationUpdateAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _organizationService.UpdateAsync(pEntity);
                _logger.LogInformation($"CoreController.OrganizationUpdateAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CoreController.OrganizationUpdateAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/core/organization/{pId}
        /// <summary>
        /// Ponto final que busca organização por codigo
        /// </summary>        
        [HttpGet]
        [Route("organization/{pId}")]
        public async Task<ActionResult<ReturnDTO>> OrganizationGetById(string pId)
        {
            _logger.LogInformation("CoreController.OrganizationGetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _organizationService.GetById(pId);
                _logger.LogInformation($"CoreController.OrganizationGetById => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CoreController.OrganizationGetById => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/core/organization
        /// <summary>
        /// Ponto final que lista todas as organizações
        /// </summary>        
        [HttpGet]
        [Route("organization")]
        public async Task<ActionResult<ReturnDTO>> OrganizationGetAll()
        {
            _logger.LogInformation("CoreController.OrganizationGetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _organizationService.GetAll();
                _logger.LogInformation($"CoreController.OrganizationGetAll => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"CoreController.OrganizationGetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }
        #endregion
    }
}
