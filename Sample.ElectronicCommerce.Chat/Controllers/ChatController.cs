using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Chat.Services;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDb;
using Sample.ElectronicCommerce.Core.Util;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.Chat.Controllers
{
    [ApiController]
    [Authorize]
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
            _logger.LogInformation("ChatBrokerController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                List<ChatMessageEntity> result = await _service.GetAll();
                returnDTO = new ReturnDTO(true, AppConstant.DeMessageSuccessWS, result);
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ChatBrokerController.GetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion
    }
}
