using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System.Threading.Tasks;
using Sample.ElectronicCommerce.Core.Util;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;

namespace Sample.ElectronicCommerce.Security.Controllers
{
    [ApiController]
    [Route("api/security")]
    public class SecurityController : ControllerBase
    {
        #region Variables
        private readonly ILogger<SecurityController> _logger;

        private readonly UserService _userService;

        private readonly JsonWebTokenService _jsonWebTokenService;
        #endregion

        #region Constructor
        public SecurityController(
            ILogger<SecurityController> logger,
            UserService service,
            JsonWebTokenService jsonWebTokenService
        )
        {
            _logger = logger;
            _userService = service;
            _jsonWebTokenService = jsonWebTokenService;
        }
        #endregion

        #region End Points
        // POST: api/security/token/auth
        /// <summary>
        /// Ponto final que autentica usuário no sistema
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [Route("token/auth")]
        public async Task<ActionResult<ReturnDTO>> Login([FromBody] UserDTO pEntity)
        {
            _logger.LogInformation("UserSessionController.Login => Start");
            ReturnDTO returnDTO = await _jsonWebTokenService.Login(pEntity);
            _logger.LogInformation($"UserSessionController.Login => IsSuccess: {returnDTO.IsSuccess} => End");
            if (returnDTO.IsSuccess)
            {
                return new OkObjectResult(returnDTO.ResultObject);
            }
            return new BadRequestObjectResult(returnDTO);
        }

        // GET: api/security/token/refresh/{pId}
        /// <summary>
        /// Ponto final que atualiza sessão de usuário
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>       
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet]
        [Route("token/refresh/{pId}")]
        public async Task<ActionResult<ReturnDTO>> Refresh(string pId)
        {
            _logger.LogInformation("UserSessionController.Refresh => Start");
            ReturnDTO returnDTO = await _jsonWebTokenService.Refresh(pId);
            _logger.LogInformation($"UserSessionController.Refresh => IsSuccess: {returnDTO.IsSuccess} => End");
            if (returnDTO.IsSuccess)
            {
                return new OkObjectResult(returnDTO.ResultObject);
            }
            return new BadRequestObjectResult(returnDTO);
        }
        #endregion

        #region End points User
        /// POST: api/security/user/lead
        /// <summary>
        /// Ponto final que insere captura usuario
        /// </summary>        
        [HttpPost]
        [Route("user/lead")]
        public async Task<ActionResult<ReturnDTO>> InsertUserLeadAsync([FromBody] UserLeadDTO pEntity)
        {
            _logger.LogInformation("SecurityController.InsertUserLeadAsync => Start");
            if (!this.ModelState.IsValid)
            {
                _logger.LogInformation("SecurityController.InsertUserLeadAsync => ModelState.IsValid: false");
                throw new BadRequestException(AppConstant.DeMessageInvalidModel);
            }
            _logger.LogInformation($"SecurityController.InsertUserLeadAsync => OK");
            return new OkObjectResult(await _userService.UserLeadInsertAsync(pEntity));
        }
        #endregion
    }
}