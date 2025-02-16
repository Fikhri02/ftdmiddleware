using Microsoft.AspNetCore.Mvc;
using FTDMiddlewareApi.Authentication;
using FTDMiddlewareDataAccess.Models.RequestModels;
using FTDMiddlewareDataAccess.Models.ResponseModels;
using FTDMiddlewareDataAccess.Models;
using System.Text.Json;
using log4net;
using FTDMiddlewareApi.Service.Interface;
using FTDMiddlewareApi.Service;

namespace FTDMiddlewareApi.Controllers
{
    [Route("api/submittrxmessage")]
    [ApiController]
    [PartnerAuth]
    public class SubmitTrxMessageController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(SubmitTrxMessageController));
        private readonly ISubmitTrxMessageService _submitTrxMessageService;

        public SubmitTrxMessageController(ISubmitTrxMessageService submitTrxMessageService)
        {
            _submitTrxMessageService = submitTrxMessageService;
        }

        [HttpPost]
        public IActionResult VerifyAmount([FromBody] SubmitTrxMessageRequest request)
        {

            SubmitTrxMessageResponse response = new SubmitTrxMessageResponse();
            response = _submitTrxMessageService.SubmitTrxMessage(request);

            var log = new GeneralLog
            {
                RequestPath = "/api/submittrxmessage",
                RequestBody = request,
                ResponseBody = response,
                Time = DateTime.UtcNow
            };

            // Serialize to JSON
            string logJson = JsonSerializer.Serialize(log, new JsonSerializerOptions
            {
                WriteIndented = true, // Pretty-print for readability
            });

            _logger.Info(logJson);


            if (response.Result != 1)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }
    }
}
