using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using System.Threading.Tasks;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Util;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;

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
            if (!this.ModelState.IsValid)
            {
                _logger.LogInformation("UserRoleController.InsertAsync => ModelState.IsValid: false");
                throw new BadRequestException(AppConstant.DeMessageInvalidModel);
            }
            _logger.LogInformation("UserRoleController.InsertAsync => End");
            return new OkObjectResult(await _service.InsertAsync(pEntity));
        }

        /// PUT: api/user/role
        /// <summary>
        /// Ponto final que atualiza permissão usuario
        /// </summary>        
        [HttpPut]
        public async Task<ActionResult<ReturnDTO>> UpdateAsync([FromBody] UserRoleEntity pEntity)
        {
            _logger.LogInformation("UserRoleController.UpdateAsync => Start");
            if (!this.ModelState.IsValid)
            {
                _logger.LogInformation("UserRoleController.UpdateAsync => ModelState.IsValid: false");
                throw new BadRequestException(AppConstant.DeMessageInvalidModel);
            }
            _logger.LogInformation($"UserRoleController.UpdateAsync => OK");
            return new OkObjectResult(await _service.UpdateAsync(pEntity));
        }

        /// DELETE: api/user/role/{pId}
        /// <summary>
        /// Ponto final que deleta permissão usuario
        /// </summary>        
        [HttpDelete]
        [Route("{pId}")]
        public async Task<ActionResult<ReturnDTO>> DeleteAsync(string pId)
        {
            _logger.LogInformation($"UserRoleController.UserRoleDeleteAsync => OK");
            return new OkObjectResult(await _service.DeleteAsync(pId));
        }

        /// GET: api/user/role/pId
        /// <summary>
        /// Ponto final que busca usuario por codigo
        /// </summary>        
        [HttpGet]
        [Route("{pId}")]
        public async Task<ActionResult<ReturnDTO>> GetByIdAsync(string pId)
        {
            _logger.LogInformation($"UserRoleController.GetByIdAsync => OK");
            return new OkObjectResult(await _service.GetById(pId));
        }

        /// GET: api/user/role
        /// <summary>
        /// Ponto final que lista todos os usuarios
        /// </summary>        
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> GetAllAsync()
        {
            _logger.LogInformation("UserRoleController.UserRoleGetAll => OK");
            return new OkObjectResult(await _service.GetAll());
        }
        #endregion
    }
}