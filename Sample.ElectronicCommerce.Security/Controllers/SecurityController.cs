﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System.Threading.Tasks;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Util;

namespace Sample.ElectronicCommerce.Security.Controllers
{
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

        /// POST: api/security/user/lead
        /// <summary>
        /// Ponto final que insere captura usuario
        /// </summary>        
        [HttpPost]
        [Route("user/lead")]
        public async Task<ActionResult<ReturnDTO>> UserLeadInsertAsync([FromBody] UserLeadDTO pEntity)
        {
            _logger.LogInformation("SecurityController.UserLeadInsertAsync => Start");
            ReturnDTO returnDTO;
            if (!this.ModelState.IsValid)
            {
                _logger.LogInformation("SecurityController.UserLeadInsertAsync => ModelState.IsValid: false");
                returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                return new BadRequestObjectResult(returnDTO);
            }
            returnDTO = await _userService.UserLeadInsertAsync(pEntity);
            _logger.LogInformation($"SecurityController.UserLeadInsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
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

        /// DELETE: api/security/user/{pId}
        /// <summary>
        /// Ponto final que deleta usuario
        /// </summary>        
        [HttpDelete]
        [Route("user/{pId}")]
        public async Task<ActionResult<ReturnDTO>> UserDeleteAsync(string pId)
        {
            ReturnDTO returnDTO = await _userService.DeleteAsync(pId);
            _logger.LogInformation($"SecurityController.UserDeleteAsync => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }

        /// GET: api/security/user/{pId}
        /// <summary>
        /// Ponto final que busca usuario por codigo
        /// </summary>        
        [HttpGet]
        [Route("user/{pId}")]
        public async Task<ActionResult<ReturnDTO>> UserGetById(string pId)
        {
            ReturnDTO returnDTO = await _userService.GetById(pId);
            _logger.LogInformation($"SecurityController.UserGetById => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }

        /// GET: api/security/user
        /// <summary>
        /// Ponto final que lista todos os usuarios
        /// </summary>        
        [HttpGet]
        [Route("user")]
        public async Task<ActionResult<ReturnDTO>> UserGetAll()
        {
            ReturnDTO returnDTO = await _userService.GetAll();
            _logger.LogInformation($"SecurityController.UserGetAll => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
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

        /// DELETE: api/security/user/role/{pId}
        /// <summary>
        /// Ponto final que deleta permissão usuario
        /// </summary>        
        [HttpDelete]
        [Route("user/role/{pId}")]
        public async Task<ActionResult<ReturnDTO>> UserRoleDeleteAsync(string pId)
        {
            ReturnDTO returnDTO = await _userRoleService.DeleteAsync(pId);
            _logger.LogInformation($"UserRoleController.UserRoleDeleteAsync => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }

        /// GET: api/security/user/role/pId
        /// <summary>
        /// Ponto final que busca usuario por codigo
        /// </summary>        
        [HttpGet]
        [Route("user/role/{pId}")]
        public async Task<ActionResult<ReturnDTO>> UserRoleGetById(string pId)
        {
            ReturnDTO returnDTO = await _userRoleService.GetById(pId);
            _logger.LogInformation($"UserRoleController.UserRoleGetById => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }

        /// GET: api/security/user/role
        /// <summary>
        /// Ponto final que lista todos os usuarios
        /// </summary>        
        [HttpGet]
        [Route("user/role")]
        public async Task<ActionResult<ReturnDTO>> UserRoleGetAll()
        {
            ReturnDTO returnDTO = await _userRoleService.GetAll();
            _logger.LogInformation($"UserRoleController.UserRoleGetAll => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }
        #endregion
    }
}
