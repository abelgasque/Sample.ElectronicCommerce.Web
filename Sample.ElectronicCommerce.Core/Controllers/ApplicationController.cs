using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Services;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Threading.Tasks;

namespace ElectronicCommerceWS.Controllers.Core
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    //[Authorize(Roles = UserRoleConstant.CodeSystem)]
    [Route("[controller]")]
    public class ApplicationController : ControllerBase
    {
        #region Variables
        private readonly ILogger<ApplicationController> _logger;

        private readonly ApplicationService _service;
        #endregion

        #region Constructor
        public ApplicationController(
            ILogger<ApplicationController> logger, 
            ApplicationService service
        ) {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points
        /// GET: Application/GetAll
        /// <summary>
        /// End Point que busca lista de aplicações ativas
        /// </summary>
        [Route("GetAll")]
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> GetAll()
        {
            _logger.LogInformation("ApplicationController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                returnDTO = await _service.GetAll();
                _logger.LogInformation($"ApplicationController.GetAll => IsSuccess: { returnDTO.IsSuccess } => End");
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ApplicationController.GetAll => Exception: { ex.Message }");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion
    }
}
