using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Core.Constants;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/token")]
    public class JsonWebTokenController : ControllerBase
    {
        #region Variables
        private readonly ILogger<JsonWebTokenController> _logger;

        private readonly JsonWebTokenService _service;
        #endregion

        #region Constructor
        public JsonWebTokenController(ILogger<JsonWebTokenController> logger, JsonWebTokenService service)
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points Token
        // POST: api/token/login
        /// <summary>
        /// End point que gera sessão de usuário
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>                
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<ReturnDTO>> Login([FromBody] UserDTO pEntity)
        {
            _logger.LogInformation("UserSessionController.Login => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.Login(pEntity);
                _logger.LogInformation($"UserSessionController.Login => IsSuccess: {returnDTO.IsSuccess} => End");
                if (returnDTO.IsSuccess)
                {
                    return new OkObjectResult(returnDTO);
                }
                return new BadRequestObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserSessionController.Login => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        // POST: api/token/refresh
        /// <summary>
        /// End point que atualiza sessão de usuário
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>             
        [HttpPost]
        [Route("refresh")]
        public async Task<ActionResult<ReturnDTO>> Refresh([FromBody] TokenDTO pEntity)
        {
            _logger.LogInformation("UserSessionController.Refresh => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.Refresh(pEntity);
                _logger.LogInformation($"UserSessionController.Refresh => IsSuccess: {returnDTO.IsSuccess} => End");
                if (returnDTO.IsSuccess)
                {
                    return new OkObjectResult(returnDTO);
                }
                return new BadRequestObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserSessionController.Refresh => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }
        #endregion
    }
}
