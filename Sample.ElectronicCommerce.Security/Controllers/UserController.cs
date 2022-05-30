using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Entities.EF.Mapping;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Controllers
{
    [ApiController]
    [Authorize(Roles = UserRoleConstant.CodeAdmin)]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        #region Variables
        private readonly ILogger<UserController> _logger;

        private readonly UserService _service;

        private readonly UserRoleService _userRoleService;
        #endregion

        #region Constructor
        public UserController(
            ILogger<UserController> logger, 
            UserService service, 
            UserRoleService userRoleService
        ) {
            _logger = logger;
            _service = service;
            _userRoleService = userRoleService;
        }
        #endregion

        #region End Points
        /// POST: User/InsertAsync
        /// <summary>
        /// End Point que insere usuario
        /// </summary>
        [HttpPost]
        [Route("InsertAsync")]
        public async Task<ActionResult<ReturnDTO>> InsertAsync([FromBody] UserEntity pEntity)
        {
            _logger.LogInformation("UserController.InsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("UserController.InsertAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _service.InsertAsync(pEntity);
                _logger.LogInformation($"UserController.InsertAsync => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController.InsertAsync => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// PUT: User/UpdateAsync
        /// <summary>
        /// End Point que atualiza usuario
        /// </summary>        
        [HttpPut]
        [Route("UpdateAsync")]
        public async Task<ActionResult<ReturnDTO>> UpdateAsync([FromBody] UserEntity pEntity)
        {
            _logger.LogInformation("UserController.UpdateAsync => Start");
            ReturnDTO returnDTO;            
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("UserController.UpdateAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _service.UpdateAsync(pEntity);
                _logger.LogInformation($"UserController.UpdateAsync => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController.UpdateAsync => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }             
        }

        /// GET: User/GetById/pId
        /// <summary>
        /// End Point que busca usuario por codigo
        /// </summary>        
        [HttpGet]
        [Route("GetById/{pId}")]
        public async Task<ActionResult<ReturnDTO>> GetById(long pId)
        {
            _logger.LogInformation("UserController.GetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetById(pId);
                _logger.LogInformation($"UserController.GetById => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController.GetById => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);                
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: User/GetAll
        /// <summary>
        /// End Point que lista todos os usuarios
        /// </summary>        
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ReturnDTO>> GetAll([FromQuery] bool? pIsActive)
        {
            _logger.LogInformation("UserController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetAll(pIsActive);
                _logger.LogInformation($"UserController.GetAll => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController.GetAll => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }

        /// GET: User/GetAll/Role
        /// <summary>
        /// End Point que lista de todas permissoes de usuarios
        /// </summary>        
        [HttpGet]
        [Route("GetAll/Role")]
        public async Task<ActionResult<ReturnDTO>> GetAllRole([FromQuery] bool? pIsActive)
        {
            _logger.LogInformation("UserController.GetAllRole => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _userRoleService.GetAll(pIsActive);
                _logger.LogInformation($"UserController.GetAllRole => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController.GetAllRole => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion
    }
}
