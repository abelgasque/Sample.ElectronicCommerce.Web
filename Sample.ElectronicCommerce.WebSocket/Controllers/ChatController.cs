using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using Sample.ElectronicCommerce.WebSocket.Entities;
using Sample.ElectronicCommerce.WebSocket.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.WebSocket.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class ChatController : ControllerBase
    {
        #region Variables
        private readonly ILogger<ChatController> _logger;

        private readonly ChatService _service;
        #endregion

        #region Constructor
        public ChatController(
            ILogger<ChatController> logger,
            ChatService service
        )
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points
        /// GET: Chat/GetAll
        /// <summary>
        /// End Point que lista todas as mensagem de chat
        /// </summary>        
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ReturnDTO>> GetAll()
        {
            _logger.LogInformation("ChatController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                List<Message> result = await _service.GetAllAsync();
                returnDTO = new ReturnDTO(true, AppConstant.DeMessageSuccessWS, result);
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"ChatController.GetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion
    }
}
