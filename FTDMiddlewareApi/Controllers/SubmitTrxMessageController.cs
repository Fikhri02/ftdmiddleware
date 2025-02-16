using Microsoft.AspNetCore.Mvc;
using FTDMiddlewareApi.Authentication;
using FTDMiddlewareDataAccess.Models.RequestModels;
using FTDMiddlewareDataAccess.Models.ResponseModels;
using FTDMiddlewareDataAccess.Models;
using System.Text.Json;
using log4net;

namespace FTDMiddlewareApi.Controllers
{
    [Route("api/submittrxmessage")]
    [ApiController]
    // [PartnerAuth]
    public class SubmitTrxMessageController : ControllerBase
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(SubmitTrxMessageController));

        [HttpPost]
        public IActionResult VerifyAmount([FromBody] SubmitTrxMessageRequest request)
        {
            // if (!ModelState.IsValid)
            // {
            //     return BadRequest(ModelState); // Returns validation errors
            // }

            SubmitTrxMessageResponse response = new SubmitTrxMessageResponse();

            long totalAmount = request.Items.Sum(item => item.UnitPrice * item.Qty);
            double mandatoryDiscount = 0;

            switch(request.TotalAmount){
                case < 200: mandatoryDiscount = 0; break;
                case >= 200 and <= 500: mandatoryDiscount = 0.05; break;
                case >= 501 and <= 800: mandatoryDiscount = 0.07; break;
                case >= 801 and <= 1200: mandatoryDiscount = 0.10; break;
                case > 1200: mandatoryDiscount = 0.15; break;
            }

            double conditionalDiscount = 0;

            if (request.TotalAmount > 500 && request.TotalAmount % 2 == 1)
            {
                conditionalDiscount += 0.08;
            }

            if (request.TotalAmount > 900 && request.TotalAmount % 10 == 5)
            {
                conditionalDiscount += 0.10;
            }

            double maxDiscount = Math.Min(mandatoryDiscount + conditionalDiscount, 0.2);

            long totalDiscount = (long)(totalAmount * maxDiscount);

            if (request.Items.Count > 0 && totalAmount != request.TotalAmount)
            {
                return BadRequest(new { message = "Invalid Total Amount." });
            }

            response.Result = 1;
            // response.ResultMessage = "Success";
            response.TotalAmount = totalAmount;
            response.TotalDiscount = totalDiscount;
            response.FinalAmount = totalAmount - totalDiscount;

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

            // Log as JSON
            _logger.Info(logJson);


            return Ok(response);
        }
    }
}
