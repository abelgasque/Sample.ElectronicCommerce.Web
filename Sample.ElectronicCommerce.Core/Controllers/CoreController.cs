using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Services;
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
        #endregion

        #region Constructor
        public CoreController(
            ILogger<CoreController> logger,
            LogAppService logAppService
            )
        {
            _logger = logger;
            _logAppService = logAppService;
        }
        #endregion

        #region End Points Log App
        /// POST: api/core/app/log
        /// <summary>
        /// Ponto final que insere historico aplicacao
        /// </summary>
        //[Authorize(Roles = UserRoleConstant.CodeAdmin)]
        [HttpPost]
        [Route("app/log")]
        public async Task<ActionResult<ReturnDTO>> LogAppInsertAsync([FromBody] LogAppEntity pEntity)
        {
            _logger.LogInformation($"CoreController.LogAppInsertAsync => Start");
            return new OkObjectResult(await _logAppService.InsertAsync(pEntity));
        }

        /// PUT: api/core/app/log
        /// <summary>
        /// Ponto final que atualiza historico aplicacao
        /// </summary>     
        [HttpPut]
        [Route("app/log")]
        public async Task<ActionResult<ReturnDTO>> LogAppUpdateAsync([FromBody] LogAppEntity pEntity)
        {
            _logger.LogInformation($"CoreController.LogAppUpdateAsync => Start");
            return new OkObjectResult(await _logAppService.UpdateAsync(pEntity));
        }

        /// GET: api/core/app/log/{pId}
        /// <summary>
        /// Ponto final que busca historico aplicacao por codigo
        /// </summary>
        [Route("app/log/{pId}")]
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> LogAppGetById(string pId)
        {
            _logger.LogInformation($"CoreController.LogAppGetById => Start");
            return new OkObjectResult(await _logAppService.GetById(pId));
        }

        /// GET: api/core/app/log
        /// <summary>
        /// Ponto final que lista todos os historicos aplicacao
        /// </summary>
        [Route("app/log")]
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> LogAppGetAll()
        {
            _logger.LogInformation($"CoreController.LogAppGetAll => Start");
            return new OkObjectResult(await _logAppService.GetAll());
        }
        #endregion
    }
}