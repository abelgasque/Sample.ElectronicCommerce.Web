using Microsoft.AspNetCore.Mvc;
using Sample.ElectronicCommerce.Security.Services;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Util;
using Sample.ElectronicCommerce.Core.Entities.Exceptions;
using System.Collections.Generic;

namespace Sample.ElectronicCommerce.Security.Controllers
{
    [ApiController]
    //[Authorize(Roles = UserRoleConstant.CodeAdmin)]
    [Route("api/user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;

        public UserController(UserService service) => _service = service;

        /// POST: api/user
        /// <summary>
        /// Ponto final que cria usuário
        /// </summary>                
        [HttpPost]
        public ActionResult<UserEntity> Create([FromBody] UserEntity pEntity)
        {
            if (!this.ModelState.IsValid) throw new BadRequestException(AppConstant.DeMessageInvalidModel);
            return new OkObjectResult(_service.Create(pEntity));
        }

        /// GET: api/user/{pId}
        /// <summary>
        /// Ponto final que busca usuario por codigo
        /// </summary>        
        [HttpGet]
        [Route("{pId}")]
        public ActionResult<UserEntity> ReadById(string pId) => new OkObjectResult(_service.ReadById(pId));

        /// GET: api/user
        /// <summary>
        /// Ponto final que busca lista de usuários
        /// </summary>        
        [HttpGet]
        public ActionResult<List<UserEntity>> ReadAll() => new OkObjectResult(_service.ReadAll());

        /// PUT: api/user
        /// <summary>
        /// Ponto final que atualiza usuario
        /// </summary>        
        [HttpPut]
        public ActionResult<UserEntity> Update([FromBody] UserEntity pEntity)
        {
            if (!this.ModelState.IsValid) throw new BadRequestException(AppConstant.DeMessageInvalidModel);
            return new OkObjectResult(_service.Update(pEntity));
        }

        /// DELETE: api/user/{pId}
        /// <summary>
        /// Ponto final que deleta usuario
        /// </summary>        
        [HttpDelete]
        [Route("{pId}")]
        public ActionResult Delete(string pId)
        {
            _service.Delete(pId);
            return new OkObjectResult(null);
        }
    }
}