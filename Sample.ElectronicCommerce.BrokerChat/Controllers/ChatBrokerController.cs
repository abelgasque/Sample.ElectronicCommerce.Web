using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.BrokerChat.Services;
using Sample.ElectronicCommerce.Core.Constants;
using Sample.ElectronicCommerce.Core.Entities.DTO;
using Sample.ElectronicCommerce.Core.Entities.MongoDb;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerChat.Controllers
{
    [ApiController]
    [Authorize]
    [Route("Chat/Broker")]
    public class ChatBrokerController : ControllerBase
    {
        #region Variables
        private readonly ILogger<ChatBrokerController> _logger;

        private readonly ChatBrokerService _service;
        #endregion

        #region Constructor
        public ChatBrokerController(
            ILogger<ChatBrokerController> logger,
            ChatBrokerService service
        )
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points
        /// GET: Chat/Broker/GetAll
        /// <summary>
        /// End Point que lista todas as mensagem de chat
        /// </summary>        
        [HttpGet]
        [Route("GetAll")]
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
