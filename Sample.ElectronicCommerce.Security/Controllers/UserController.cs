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
            if (!this.ModelState.IsValid)
            {
                _logger.LogInformation("UserController.InsertAsync => ModelState.IsValid: false");
                throw new BadRequestException(AppConstant.DeMessageInvalidModel);
            }
            _logger.LogInformation($"UserController.InsertAsync => OK");
            return new OkObjectResult(await _service.InsertAsync(pEntity));
        }

        /// PUT: api/user
        /// <summary>
        /// Ponto final que atualiza usuario
        /// </summary>        
        [HttpPut]
        public async Task<ActionResult<ReturnDTO>> UpdateAsync([FromBody] UserEntity pEntity)
        {
            _logger.LogInformation("UserController.UpdateAsync => Start");
            if (!this.ModelState.IsValid)
            {
                _logger.LogInformation("UserController.UpdateAsync => ModelState.IsValid: false");
                throw new BadRequestException(AppConstant.DeMessageInvalidModel);
            }
            _logger.LogInformation("UserController.UpdateAsync => OK");
            return new OkObjectResult(await _service.UpdateAsync(pEntity));
        }

        /// DELETE: api/user/{pId}
        /// <summary>
        /// Ponto final que deleta usuario
        /// </summary>        
        [HttpDelete]
        [Route("{pId}")]
        public async Task<ActionResult<ReturnDTO>> DeleteAsync(string pId)
        {
            _logger.LogInformation("UserController.DeleteAsync => Start");
            return new OkObjectResult(await _service.DeleteAsync(pId));
        }

        /// GET: api/user/{pId}
        /// <summary>
        /// Ponto final que busca usuario por codigo
        /// </summary>        
        [HttpGet]
        [Route("{pId}")]
        public async Task<ActionResult<ReturnDTO>> GetByIdAsync(string pId)
        {
            _logger.LogInformation("UserController.GetByIdAsync => Start");
            return new OkObjectResult(await _service.GetById(pId));
        }

        /// GET: api/user
        /// <summary>
        /// Ponto final que lista todos os usuarios
        /// </summary>        
        [HttpGet]
        public async Task<ActionResult<ReturnDTO>> GetAllAsync()
        {
            _logger.LogInformation("UserController.GetAllAsync => OK");
            return new OkObjectResult(await _service.GetAll());
        }
        #endregion
    }
}