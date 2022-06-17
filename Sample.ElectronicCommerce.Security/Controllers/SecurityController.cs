using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System;
using System.Net;
using System.Threading.Tasks;
using Sample.ElectronicCommerce.Core.Entities.MongoDb;
using Sample.ElectronicCommerce.Core.Util;
using Microsoft.AspNetCore.Authorization;

namespace Sample.ElectronicCommerce.Security.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/security")]
    public class SecurityController : ControllerBase
    {
        #region Variables
        private readonly ILogger<SecurityController> _logger;

        private readonly UserService _userService;

        private readonly UserRoleService _userRoleService;

        private readonly JsonWebTokenService _jsonWebTokenService;
        #endregion

        #region Constructor
        public SecurityController(
            ILogger<SecurityController> logger,
            UserService service,
            UserRoleService userRoleService,
            JsonWebTokenService jsonWebTokenService
        )
        {
            _logger = logger;
            _userService = service;
            _userRoleService = userRoleService;
            _jsonWebTokenService = jsonWebTokenService;
        }
        #endregion

        #region End Points
        // POST: api/security/token/login
        /// <summary>
        /// Ponto final que autentica usuário no sistema
        /// </summary>
        /// <param name="pEntity"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("token/login")]
        public async Task<ActionResult<ReturnDTO>> Login([FromBody] UserDTO pEntity)
        {
            _logger.LogInformation("UserSessionController.Login => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _jsonWebTokenService.Login(pEntity);
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

        // POST: api/security/token/refresh
        /// <summary>
        /// Ponto final que atualiza sessão de usuário
        /// </summary>
        /// <param name="pId"></param>
        /// <returns></returns>
        [AllowAnonymous]           
        [HttpPost]
        [Route("token/refresh")]
        public async Task<ActionResult<ReturnDTO>> Refresh([FromBody] string pId)
        {
            _logger.LogInformation("UserSessionController.Refresh => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _jsonWebTokenService.Refresh(pId);
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

        #region End points User
        /// POST: api/security/user
        /// <summary>
        /// Ponto final que insere usuario
        /// </summary>        
        //[Authorize(Roles = UserRoleConstant.CodeAdmin)]
        [HttpPost]
        [Route("user")]
        public async Task<ActionResult<ReturnDTO>> UserInsertAsync([FromBody] UserEntity pEntity)
        {
            _logger.LogInformation("SecurityController.UserInsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("SecurityController.UserInsertAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _userService.InsertAsync(pEntity);
                _logger.LogInformation($"SecurityController.UserInsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SecurityController.UserInsertAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// PUT: api/security/user
        /// <summary>
        /// Ponto final que atualiza usuario
        /// </summary>        
        [HttpPut]
        [Route("user")]
        public async Task<ActionResult<ReturnDTO>> UserUpdateAsync([FromBody] UserEntity pEntity)
        {
            _logger.LogInformation("SecurityController.UserUpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("SecurityController.UserUpdateAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _userService.UpdateAsync(pEntity);
                _logger.LogInformation($"SecurityController.UserUpdateAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SecurityController.UserUpdateAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// DELETE: api/security/user/{pId}
        /// <summary>
        /// Ponto final que deleta usuario
        /// </summary>        
        [HttpDelete]
        [Route("user/{pId}")]
        public async Task<ActionResult<ReturnDTO>> UserDeleteAsync([FromBody] string pId)
        {
            _logger.LogInformation("SecurityController.UserDeleteAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _userService.DeleteAsync(pId);
                _logger.LogInformation($"SecurityController.UserDeleteAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SecurityController.UserDeleteAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/security/user/{pId}
        /// <summary>
        /// Ponto final que busca usuario por codigo
        /// </summary>        
        [HttpGet]
        [Route("user/{pId}")]
        public async Task<ActionResult<ReturnDTO>> UserGetById(string pId)
        {
            _logger.LogInformation("SecurityController.UserGetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _userService.GetById(pId);
                _logger.LogInformation($"SecurityController.UserGetById => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SecurityController.UserGetById => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/security/user
        /// <summary>
        /// Ponto final que lista todos os usuarios
        /// </summary>        
        [HttpGet]
        [Route("user")]
        public async Task<ActionResult<ReturnDTO>> UserGetAll()
        {
            _logger.LogInformation("SecurityController.UserGetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _userService.GetAll();
                _logger.LogInformation($"SecurityController.UserGetAll => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SecurityController.UserGetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }
        #endregion

        #region End points
        /// POST: api/security/user/role
        /// <summary>
        /// Ponto final que insere permissão usuario
        /// </summary>
        [HttpPost]
        [Route("user/role")]
        public async Task<ActionResult<ReturnDTO>> UserRoleInsertAsync([FromBody] UserRoleEntity pEntity)
        {
            _logger.LogInformation("UserRoleController.UserRoleInsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("UserRoleController.UserRoleInsertAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _userRoleService.InsertAsync(pEntity);
                _logger.LogInformation($"UserRoleController.UserRoleInsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserRoleController.UserRoleInsertAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// PUT: api/security/user/role
        /// <summary>
        /// Ponto final que atualiza permissão usuario
        /// </summary>        
        [HttpPut]
        [Route("user/role")]
        public async Task<ActionResult<ReturnDTO>> UserRoleUpdateAsync([FromBody] UserRoleEntity pEntity)
        {
            _logger.LogInformation("UserRoleController.UserRoleUpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("UserRoleController.UserRoleUpdateAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _userRoleService.UpdateAsync(pEntity);
                _logger.LogInformation($"UserRoleController.UserRoleUpdateAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserRoleController.UserRoleUpdateAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// DELETE: api/security/user/role/{pId}
        /// <summary>
        /// Ponto final que deleta permissão usuario
        /// </summary>        
        [HttpDelete]
        [Route("user/role/{pId}")]
        public async Task<ActionResult<ReturnDTO>> UserRoleDeleteAsync([FromBody] string pId)
        {
            _logger.LogInformation("UserRoleController.UserRoleDeleteAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _userRoleService.DeleteAsync(pId);
                _logger.LogInformation($"UserRoleController.UserRoleDeleteAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserRoleController.UserRoleDeleteAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/security/user/role/pId
        /// <summary>
        /// Ponto final que busca usuario por codigo
        /// </summary>        
        [HttpGet]
        [Route("user/role/{pId}")]
        public async Task<ActionResult<ReturnDTO>> UserRoleGetById(string pId)
        {
            _logger.LogInformation("UserRoleController.UserRoleGetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _userRoleService.GetById(pId);
                _logger.LogInformation($"UserRoleController.UserRoleGetById => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserRoleController.UserRoleGetById => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/security/user/role
        /// <summary>
        /// Ponto final que lista todos os usuarios
        /// </summary>        
        [HttpGet]
        [Route("user/role")]
        public async Task<ActionResult<ReturnDTO>> UserRoleGetAll()
        {
            _logger.LogInformation("UserRoleController.UserRoleGetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _userRoleService.GetAll();
                _logger.LogInformation($"UserRoleController.UserRoleGetAll => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserRoleController.UserRoleGetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }
        #endregion
    }
}
