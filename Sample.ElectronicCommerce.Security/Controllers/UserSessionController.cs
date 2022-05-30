using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Controllers
{    
    [ApiController]    
    [Route("[controller]")]
    public class UserSessionController : ControllerBase
    {
        #region Variables
        private readonly ILogger<UserSessionController> _logger;

        private readonly UserSessionService _service;
        #endregion

        #region Constructor
        public UserSessionController(ILogger<UserSessionController> logger, UserSessionService service)
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points
        /// GET: Core/UserSession/GetById/pId
        /// <summary>
        /// End Point que busca sessao usuario por codigo
        /// </summary>             
        [HttpGet]
        [Authorize(Roles = UserRoleConstant.CodeAdmin)]
        [Route("GetById/{pId}")]
        public async Task<ActionResult<ReturnDTO>> GetById(long pId)
        {
            _logger.LogInformation("UserSessionController.GetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetById(pId);
                _logger.LogInformation($"UserSessionController.GetById => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserSessionController.GetById => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: Core/UserSession/GetAll
        /// <summary>
        /// End Point que lista todos os sessoes usuarios
        /// </summary>          
        [HttpGet]
        [Authorize(Roles = UserRoleConstant.CodeAdmin)]
        [Route("GetAll")]
        public async Task<ActionResult<ReturnDTO>> GetAll([FromQuery] bool? pIsActive)
        {
            _logger.LogInformation("UserSessionController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetAll(pIsActive);
                _logger.LogInformation($"UserSessionController.GetAll => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserSessionController.GetAll => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion        
    }
}
