using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Entities.DataBase.Mapping;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Services;
using Sample.ElectronicCommerce.Core.Util;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Controllers
{
    [ApiController]
    [Route("api/core")]
    public class CoreController : ControllerBase
    {
        #region Variables
        private readonly ILogger<CoreController> _logger;

        private readonly LogAppService _logAppService;

        private readonly OrganizationService _organizationService;
        #endregion

        #region Constructor
        public CoreController(
            ILogger<CoreController> logger,
            LogAppService service,
            OrganizationService organizationService
            )
        {
            _logger = logger;
            _logAppService = service;
            _organizationService = organizationService;
        }
        #endregion

        #region End Points Log App
        /// POST: log-app
        /// <summary>
        /// Ponto final que insere historico aplicacao
        /// </summary>
        //[Authorize(Roles = UserRoleConstant.CodeAdmin)]
        [HttpPost]
        [Route("log-app")]
        public async Task<ActionResult<ReturnDTO>> LogAppInsertAsync([FromBody] LogAppEntity pEntity)
        {
            ReturnDTO returnDTO = await _logAppService.InsertAsync(pEntity);
            _logger.LogInformation($"CoreController.LogAppInsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }

        /// PUT: log-app
        /// <summary>
        /// Ponto final que atualiza historico aplicacao
        /// </summary>     
        [HttpPut]
        [Route("log-app")]
        public async Task<ActionResult<ReturnDTO>> LogAppUpdateAsync([FromBody] LogAppEntity pEntity)
        {
            ReturnDTO returnDTO = await _logAppService.UpdateAsync(pEntity);
            _logger.LogInformation($"CoreController.LogAppUpdateAsync => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }

        /// GET: log-app/{pId}
        /// <summary>
        /// Ponto final que busca historico aplicacao por codigo
        /// </summary>
        [Route("log-app/{pId}")]
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> LogAppGetById(string pId)
        {
            ReturnDTO returnDTO = await _logAppService.GetById(pId);
            _logger.LogInformation($"CoreController.LogAppGetById => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }

        /// GET: log-app
        /// <summary>
        /// Ponto final que lista todos os historicos aplicacao
        /// </summary>
        [Route("log-app")]
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> LogAppGetAll()
        {
            ReturnDTO returnDTO = await _logAppService.GetAll();
            _logger.LogInformation($"CoreController.LogAppGetAll => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }
        #endregion

        #region End Points Organization
        /// POST: api/core/organization
        /// <summary>
        /// Ponto final que insere organização
        /// </summary>
        [HttpPost]
        [Route("organization")]
        public async Task<ActionResult<ReturnDTO>> OrganizationInsertAsync([FromBody] OrganizationEntity pEntity)
        {
            _logger.LogInformation("CoreController.OrganizationInsertAsync => Start");
            ReturnDTO returnDTO;
            if (!this.ModelState.IsValid)
            {
                _logger.LogInformation("CoreController.OrganizationInsertAsync => ModelState.IsValid: false");
                returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                return new BadRequestObjectResult(returnDTO);
            }
            returnDTO = await _organizationService.InsertAsync(pEntity);
            _logger.LogInformation($"CoreController.OrganizationInsertAsync => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }

        /// PUT: api/core/organization
        /// <summary>
        /// Ponto final que atualiza organização
        /// </summary>        
        [HttpPut]
        [Route("organization")]
        public async Task<ActionResult<ReturnDTO>> OrganizationUpdateAsync([FromBody] OrganizationEntity pEntity)
        {
            _logger.LogInformation("CoreController.OrganizationUpdateAsync => Start");
            ReturnDTO returnDTO;
            if (!this.ModelState.IsValid)
            {
                _logger.LogInformation("CoreController.OrganizationUpdateAsync => ModelState.IsValid: false");
                returnDTO = new ReturnDTO(false, AppConstant.DeMessageInvalidModel, this.ModelState);
                return new BadRequestObjectResult(returnDTO);
            }
            returnDTO = await _organizationService.UpdateAsync(pEntity);
            _logger.LogInformation($"CoreController.OrganizationUpdateAsync => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }

        /// GET: api/core/organization/{pId}
        /// <summary>
        /// Ponto final que busca organização por codigo
        /// </summary>        
        [HttpGet]
        [Route("organization/{pId}")]
        public async Task<ActionResult<ReturnDTO>> OrganizationGetById(string pId)
        {
            ReturnDTO returnDTO = await _organizationService.GetById(pId);
            _logger.LogInformation($"CoreController.OrganizationGetById => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }

        /// GET: api/core/organization
        /// <summary>
        /// Ponto final que lista todas as organizações
        /// </summary>        
        [HttpGet]
        [Route("organization")]
        public async Task<ActionResult<ReturnDTO>> OrganizationGetAll()
        {
            ReturnDTO returnDTO = await _organizationService.GetAll();
            _logger.LogInformation($"CoreController.OrganizationGetAll => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }
        #endregion
    }
}