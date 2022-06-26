using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Services;
using Sample.ElectronicCommerce.Core.Util;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Core.Controllers
{
    [ApiController]
    [Route("api/organization")]
    public class OrganizationController : ControllerBase
    {
        #region Variables
        private readonly ILogger<OrganizationController> _logger;

        private readonly LogAppService _logAppService;

        private readonly OrganizationService _organizationService;
        #endregion

        #region Constructor
        public OrganizationController(
            ILogger<OrganizationController> logger,
            LogAppService logAppService,
            OrganizationService organizationService
            )
        {
            _logger = logger;
            _logAppService = logAppService;
            _organizationService = organizationService;
        }
        #endregion

        #region End Points Methods
        /// POST: api/organization
        /// <summary>
        /// Ponto final que insere organização
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<ReturnDTO>> InsertAsync([FromBody] OrganizationEntity pEntity)
        {
            _logger.LogInformation("OrganizationController.InsertAsync => Start");
            if (!this.ModelState.IsValid)
            {
                _logger.LogInformation("OrganizationController.InsertAsync => ModelState.IsValid: false");
                throw new BadRequestException(AppConstant.DeMessageInvalidModel);
            }
            _logger.LogInformation($"OrganizationController.InsertAsync => OK");
            return new OkObjectResult(await _organizationService.InsertAsync(pEntity));
        }

        /// PUT: api/organization
        /// <summary>
        /// Ponto final que atualiza organização
        /// </summary>        
        [HttpPut]
        public async Task<ActionResult<ReturnDTO>> UpdateAsync([FromBody] OrganizationEntity pEntity)
        {
            _logger.LogInformation("OrganizationController.UpdateAsync => Start");
            if (!this.ModelState.IsValid)
            {
                _logger.LogInformation("OrganizationController.UpdateAsync => ModelState.IsValid: false");
                throw new BadRequestException(AppConstant.DeMessageInvalidModel);
            }
            _logger.LogInformation($"OrganizationController.UpdateAsync => OK");
            return new OkObjectResult(await _organizationService.UpdateAsync(pEntity));
        }

        /// GET: api/organization/{pId}
        /// <summary>
        /// Ponto final que busca organização por codigo
        /// </summary>        
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> GetById(string pId)
        {
            ReturnDTO returnDTO = await _organizationService.GetById(pId);
            _logger.LogInformation($"OrganizationController.GetById => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }

        /// GET: api/organization
        /// <summary>
        /// Ponto final que lista todas as organizações
        /// </summary>        
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> GetAll()
        {
            ReturnDTO returnDTO = await _organizationService.GetAll();
            _logger.LogInformation($"OrganizationController.GetAll => IsSuccess: {returnDTO.IsSuccess} => End");
            return new OkObjectResult(returnDTO);
        }
        #endregion
    }
}