using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Constants;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.Mapping;
using Sample.ElectronicCommerce.Core.Services;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Controllers
{
    [ApiController]
    [Authorize(Roles = UserRoleConstant.CodeAdmin)]
    [Route("[controller]")]
    public class LogAppController : ControllerBase
    {
        #region Variables
        private readonly ILogger<LogAppController> _logger;

        private readonly LogAppService _service;
        #endregion

        #region Constructor
        public LogAppController(ILogger<LogAppController> logger, LogAppService service)
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points
        /// POST: LogApp/InsertAsync
        /// <summary>
        /// End Point que insere historico aplicacao
        /// </summary>
        [Route("InsertAsync")]        
        [HttpPost]
        public async Task<ActionResult<ReturnDTO>> InsertAsync([FromBody] LogAppEntity pEntity)
        {
            _logger.LogInformation("LogAppController.InsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.InsertAsync(pEntity);
                _logger.LogInformation($"LogAppController.InsertAsync => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"LogAppController.InsertAsync => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// PUT: LogApp/UpdateAsync
        /// <summary>
        /// End Point que atualiza historico aplicacao
        /// </summary>
        [Route("UpdateAsync")]        
        [HttpPut]
        public async Task<ActionResult<ReturnDTO>> UpdateAsync([FromBody] LogAppEntity pEntity)
        {
            _logger.LogInformation("LogAppController.UpdateAsync => Start");
            ReturnDTO returnDTO;
            try 
            {     
                returnDTO = await _service.UpdateAsync(pEntity);
                _logger.LogInformation($"LogAppController.UpdateAsync => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"LogAppController.UpdateAsync => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: LogApp/GetById/pId
        /// <summary>
        /// End Point que busca historico aplicacao por codigo
        /// </summary>
        [Route("GetById/{pId}")]        
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> GetById(long pId)
        {
            _logger.LogInformation("LogAppController.GetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetById(pId);
                _logger.LogInformation($"LogAppController.GetById => IsSuccess: { returnDTO.IsSuccess } => End");                
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"LogAppController.GetById => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }            
        }

        /// GET: LogApp/GetAll
        /// <summary>
        /// End Point que lista todos os historicos aplicacao
        /// </summary>
        [Route("GetAll")]        
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> GetAll([FromQuery] bool? pIsActive)
        {
            _logger.LogInformation("LogAppController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {               
                returnDTO = await _service.GetAll(pIsActive);
                _logger.LogInformation($"LogAppController.GetAll => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"LogAppController.GetAll => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: LogApp/GetLogAppForChartDynamic
        /// <summary>
        /// End Point que exibe grafico de linha mensal do historico do sistema
        /// </summary>
        [Route("GetLogAppForChartDynamic")]        
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> GetLogAppForChartMonth([FromQuery]  bool pMustFilterYear)
        {
            _logger.LogInformation("LogAppController.GetLogAppForChartMonth => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetLogAppForChartDynamic(pMustFilterYear);
                _logger.LogInformation($"LogAppController.GetLogAppForChartMonth => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"LogAppController.GetLogAppForChartMonth => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: LogApp/GetLogAppDay
        /// <summary>
        /// End Point consulta de logs diarios do sistema
        /// </summary>
        [Route("GetLogAppDay")]        
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> GetLogAppDay()
        {
            _logger.LogInformation("LogAppController.GetLogAppDay => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetLogAppDay();
                _logger.LogInformation($"LogAppController.GetLogAppDay => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"LogAppController.GetLogAppDay => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion
    }
}
