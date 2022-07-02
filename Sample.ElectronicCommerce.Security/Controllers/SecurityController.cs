using Microsoft.AspNetCore.Mvc;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Util;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;

namespace Sample.ElectronicCommerce.Security.Controllers
{
    [ApiController]
    [Route("api/security")]
    public class SecurityController : ControllerBase
    {
        private readonly JsonWebTokenService _service;

        private readonly UserService _userService;

        public SecurityController(
            JsonWebTokenService service,
            UserService userService
        )
        {
            _service = service;
            _userService = userService;
        }

        /// POST: api/security/token/auth
        /// <summary>
        /// Ponto final que autentica usuário e gera token acesso do usuário
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [Route("token/auth")]
        public ActionResult<TokenDTO> Login([FromBody] UserDTO pEntity)
        {
            return new OkObjectResult(_service.Login(pEntity));
        }

        /// POST: api/security/token/refresh/{pId}
        /// <summary>
        /// Ponto final que atualiza token acesso do usuário
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>       
        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpPost]
        [Route("token/refresh")]
        public ActionResult<TokenDTO> Refresh([FromBody] TokenDTO pEntity)
        {
            return new OkObjectResult(_service.Refresh(pEntity));
        }

        /// POST: api/security/user/lead
        /// <summary>
        /// Ponto final que cria captura usuario
        /// </summary>     
        /// /// <param name="pEntity"></param>
        [HttpPost]
        [Route("user/lead")]
        public ActionResult CreateLeadAsync([FromBody] UserLeadDTO pEntity)
        {
            if (!this.ModelState.IsValid) throw new BadRequestException(AppConstant.DeMessageInvalidModel);
            _userService.CreateLeadAsync(pEntity);
            return new OkObjectResult(null);
        }
    }
}