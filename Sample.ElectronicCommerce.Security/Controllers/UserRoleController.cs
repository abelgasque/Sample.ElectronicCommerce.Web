using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Entities;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Core.Constants;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Security.Controllers
{
    [ApiController]
    //[Authorize(Roles = UserRoleConstant.CodeAdmin)]
    [Route("api/user/role")]
    public class UserRoleController : ControllerBase
    {
        #region Variables
        private readonly ILogger<UserRoleController> _logger;

        private readonly UserRoleService _service;
        #endregion

        #region Constructor
        public UserRoleController(
            ILogger<UserRoleController> logger,
            UserRoleService service
        )
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End points
        /// POST: api/user/role
        /// <summary>
        /// Ponto final que insere permissão usuario
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ReturnDTO>> InsertAsync([FromBody] UserRoleEntity pEntity)
        {
            _logger.LogInformation("UserRoleController.InsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("UserRoleController.InsertAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _service.InsertAsync(pEntity);
                _logger.LogInformation($"UserRoleController.InsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserRoleController.InsertAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// PUT: api/user/role
        /// <summary>
        /// Ponto final que atualiza permissão usuario
        /// </summary>        
        [HttpPut]
        public async Task<ActionResult<ReturnDTO>> UpdateAsync([FromBody] UserRoleEntity pEntity)
        {
            _logger.LogInformation("UserRoleController.UpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("UserRoleController.UpdateAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _service.UpdateAsync(pEntity);
                _logger.LogInformation($"UserRoleController.UpdateAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserRoleController.UpdateAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// DELETE: api/user/role/pId
        /// <summary>
        /// Ponto final que deleta permissão usuario
        /// </summary>        
        [HttpDelete]
        public async Task<ActionResult<ReturnDTO>> DeleteAsync([FromBody] string pId)
        {
            _logger.LogInformation("UserRoleController.DeleteAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.DeleteAsync(pId);
                _logger.LogInformation($"UserRoleController.DeleteAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserRoleController.DeleteAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/user/role/pId
        /// <summary>
        /// Ponto final que busca usuario por codigo
        /// </summary>        
        [HttpGet]
        [Route("{pId}")]
        public async Task<ActionResult<ReturnDTO>> GetById(string pId)
        {
            _logger.LogInformation("UserRoleController.GetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetById(pId);
                _logger.LogInformation($"UserRoleController.GetById => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserRoleController.GetById => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/user/role
        /// <summary>
        /// Ponto final que lista todos os usuarios
        /// </summary>        
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> GetAll()
        {
            _logger.LogInformation("UserRoleController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetAll();
                _logger.LogInformation($"UserRoleController.GetAll => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserRoleController.GetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }
        #endregion
    }
}
