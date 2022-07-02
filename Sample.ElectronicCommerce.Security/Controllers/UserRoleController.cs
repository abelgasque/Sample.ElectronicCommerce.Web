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
    [Route("api/user/role")]
    public class UserRoleController : ControllerBase
    {
        private readonly UserRoleService _service;

        public UserRoleController(UserRoleService service) => _service = service;

        #region End points
        /// POST: api/user/role
        /// <summary>
        /// Ponto final que cria permissão usuario
        /// </summary>
        [HttpPost]
        public ActionResult<UserRoleEntity> Create([FromBody] UserRoleEntity pEntity)
        {
            if (!this.ModelState.IsValid) throw new BadRequestException(AppConstant.DeMessageInvalidModel);
            return new OkObjectResult(_service.Create(pEntity));
        }

        /// GET: api/user/role/pId
        /// <summary>
        /// Ponto final que busca permissão de usuário por código
        /// </summary>        
        [HttpGet]
        [Route("{pId}")]
        public ActionResult<UserRoleEntity> ReadById(string pId) => new OkObjectResult(_service.ReadById(pId));

        /// GET: api/user/role
        /// <summary>
        /// Ponto final que busca as permissões de usuario
        /// </summary>        
        [HttpGet]
        public ActionResult<List<UserRoleEntity>> ReadAll() => new OkObjectResult(_service.ReadAll());

        /// PUT: api/user/role
        /// <summary>
        /// Ponto final que atualiza permissão usuário
        /// </summary>        
        [HttpPut]
        public ActionResult<UserRoleEntity> Update([FromBody] UserRoleEntity pEntity)
        {
            if (!this.ModelState.IsValid) throw new BadRequestException(AppConstant.DeMessageInvalidModel);
            return new OkObjectResult(_service.Update(pEntity));
        }

        /// DELETE: api/user/role/{pId}
        /// <summary>
        /// Ponto final que deleta permissão usuário
        /// </summary>        
        [HttpDelete]
        [Route("{pId}")]
        public ActionResult DeleteAsync(string pId)
        {
            _service.Delete(pId);
            return new OkObjectResult(null);
        }
        #endregion
    }
}