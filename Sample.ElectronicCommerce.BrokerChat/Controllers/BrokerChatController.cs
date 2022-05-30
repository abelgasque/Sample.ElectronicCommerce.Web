using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sample.ElectronicCommerce.BrokerChat.Entities;
using Sample.ElectronicCommerce.BrokerChat.Services;
using Sample.ElectronicCommerce.Shared.Constants;
using Sample.ElectronicCommerce.Shared.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sample.ElectronicCommerce.BrokerChat.Controllers
{
    [ApiController]
    [Authorize]
    [Route("[controller]")]
    public class BrokerChatController : ControllerBase
    {
        #region Variables
        private readonly ILogger<BrokerChatController> _logger;

        private readonly BrokerChatService _service;
        #endregion

        #region Constructor
        public BrokerChatController(
            ILogger<BrokerChatController> logger,
            BrokerChatService service
        )
        {
            _logger = logger;
            _service = service;
        }
        #endregion

        #region End Points
        /// GET: BrokerChat/GetAll
        /// <summary>
        /// End Point que lista todas as mensagem de chat
        /// </summary>        
        [HttpGet]
        [Route("GetAll")]
        public async Task<ActionResult<ReturnDTO>> GetAll()
        {
            _logger.LogInformation("BrokerChatController.GetAll => Start");
            ReturnDTO returnDTO;
            try
            {
                List<BrokerChatEntity> result = await _service.GetAll();
                returnDTO = new ReturnDTO(true, AppConstant.DeMessageSuccessWS, result);
                return new OkObjectResult(returnDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError($"BrokerChatController.GetAll => Exception: {ex.Message}");
                returnDTO = new ReturnDTO(false, AppConstant.ServerExceptionHandlerMessageWS, ex);
                return new BadRequestObjectResult(returnDTO);
            }
        }
        #endregion
    }
}
