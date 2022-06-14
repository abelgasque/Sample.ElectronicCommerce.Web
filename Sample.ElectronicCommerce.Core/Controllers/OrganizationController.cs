using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Constants;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDb;
using Sample.ElectronicCommerce.Core.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ElectronicCommerceWS.Controllers.Core
{
    [ApiController]   
    //[Authorize(Roles = OrganizationRoleConstant.CodeSystem)]
    [Route("[controller]")]
    public class OrganizationController : ControllerBase
    {
        #region Variables
        private readonly ILogger<OrganizationController> _logger;

        private readonly OrganizationService _service;
        #endregion

        #region Constructor
        public OrganizationController(
            ILogger<OrganizationController> logger, 
            OrganizationService service
        ) {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points
        /// POST: Organization/InsertAsync
        /// <summary>
        /// End Point que insere aplicação
        /// </summary>
        [HttpPost]
        [Route("InsertAsync")]
        public async Task<ActionResult<ReturnDTO>> InsertAsync([FromBody] OrganizationEntity pEntity)
        {
            _logger.LogInformation("OrganizationController.InsertAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("OrganizationController.InsertAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _service.InsertAsync(pEntity);
                _logger.LogInformation($"OrganizationController.InsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"OrganizationController.InsertAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// PUT: Organization/UpdateAsync
        /// <summary>
        /// End Point que atualiza aplicação
        /// </summary>        
        [HttpPut]
        [Route("UpdateAsync")]
        public async Task<ActionResult<ReturnDTO>> UpdateAsync([FromBody] OrganizationEntity pEntity)
        {
            _logger.LogInformation("OrganizationController.UpdateAsync => Start");
            ReturnDTO returnDTO;
            try
            {
                if (!this.ModelState.IsValid)
                {
                    _logger.LogInformation("OrganizationController.UpdateAsync => ModelState.IsValid: false");
                    returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                    return new BadRequestObjectResult(returnDTO);
                }
                returnDTO = await _service.UpdateAsync(pEntity);
                _logger.LogInformation($"OrganizationController.UpdateAsync => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"OrganizationController.UpdateAsync => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: Organization/GetById/pId
        /// <summary>
        /// End Point que busca aplicação por codigo
        /// </summary>        
        [HttpGet]
        [Route("GetById/{pId}")]
        public async Task<ActionResult<ReturnDTO>> GetById(string pId)
        {
            _logger.LogInformation("OrganizationController.GetById => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetById(pId);
                _logger.LogInformation($"OrganizationController.GetById => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"OrganizationController.GetById => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }

        /// GET: Organization/GetAll
        /// <summary>
        /// End Point que lista todos os aplicações ativas
        /// </summary>        
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ReturnDTO>> GetAll()
        {
            _logger.LogInformation("OrganizationController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetAll();
                _logger.LogInformation($"OrganizationController.GetAll => IsSuccess: {returnDTO.IsSuccess} => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"OrganizationController.GetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, returnDTO);
            }
        }
        #endregion
    }
}
