using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("[controller]")]
    public class TokenController : ControllerBase
    {
        #region Variables
        private readonly ILogger<TokenController> _logger;

        private readonly UserSessionService _service;
        #endregion

        #region Constructor
        public TokenController(ILogger<TokenController> logger, UserSessionService service)
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points Token
        // POST: Token/Login
        /// <summary>
        /// End point que gera sessão de usuário
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>                
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult<ReturnDTO>> Login([FromBody] UserDTO pEntity)
        {
            _logger.LogInformation("UserSessionController.Login => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.Login(pEntity, false);
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
        
        // POST: Token/Refresh
        /// <summary>
        /// End point que atualiza sessão de usuário
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>             
        [HttpPost]
        [Route("Refresh")]
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
