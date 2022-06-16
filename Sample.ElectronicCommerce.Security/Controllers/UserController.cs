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
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        #region Variables
        private readonly ILogger<UserController> _logger;

        private readonly UserService _service;
        #endregion

        #region Constructor
        public UserController(
            ILogger<UserController> logger,
            UserService service
        )
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End points
        /// POST: api/user
        /// <summary>
        /// Ponto final que insere usuario
        /// </summary>
        [HttpPost]
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
                _logger.LogInformation($"UserController.InsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController.InsertAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// PUT: api/user
        /// <summary>
        /// Ponto final que atualiza usuario
        /// </summary>        
        [HttpPut]
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
                _logger.LogInformation($"UserController.UpdateAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController.UpdateAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// DELETE: api/user/pId
        /// <summary>
        /// Ponto final que deleta usuario
        /// </summary>        
        [HttpDelete]
        public async Task<ActionResult<ReturnDTO>> DeleteAsync([FromBody] string pId)
        {
            _logger.LogInformation("UserController.DeleteAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.DeleteAsync(pId);
                _logger.LogInformation($"UserController.DeleteAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController.DeleteAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/user/pId
        /// <summary>
        /// Ponto final que busca usuario por codigo
        /// </summary>        
        [HttpGet]
        [Route("{pId}")]
        public async Task<ActionResult<ReturnDTO>> GetById(string pId)
        {
            _logger.LogInformation("UserController.GetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetById(pId);
                _logger.LogInformation($"UserController.GetById => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController.GetById => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: api/user
        /// <summary>
        /// Ponto final que lista todos os usuarios ativos
        /// </summary>        
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ReturnDTO>> GetAll()
        {
            _logger.LogInformation("UserController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetAll();
                _logger.LogInformation($"UserController.GetAll => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"UserController.GetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }
        #endregion
    }
}
