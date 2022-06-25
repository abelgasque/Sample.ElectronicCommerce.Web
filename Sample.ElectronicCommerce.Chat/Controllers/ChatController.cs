using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Chat.Services;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDB;
using Sample.ElectronicCommerce.Core.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Chat.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatController : ControllerBase
    {
        #region Variables
        private readonly ILogger<ChatController> _logger;

        private readonly ChatBrokerService _service;
        #endregion

        #region Constructor
        public ChatController(
            ILogger<ChatController> logger,
            ChatBrokerService service
        )
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points
        /// GET: api/chat/broker
        /// <summary>
        /// End Point que lista todas as mensagem de chat
        /// </summary>        
        [HttpGet]
        [Route("broker")]
        public async Task<ActionResult<ReturnDTO>> GetAll()
        {
            List<ChatMessageEntity> result = await _service.GetAll();
            ReturnDTO returnDTO = new ReturnDTO(true, AppConstant.DeMessageSuccessWS, result);
            return new OkObjectResult(returnDTO);
        }
        #endregion
    }
}
