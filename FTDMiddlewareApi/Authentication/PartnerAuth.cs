using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using FTDMiddlewareDataAccess.Models.RequestModels;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace FTDMiddlewareApi.Authentication
{
    public class PartnerAuthAttribute : ActionFilterAttribute
    {
        // Simulating a database of allowed partners
        private readonly Dictionary<string, (string AllowedPartner, string AllowedPassword)> _allowedPartners =
            new Dictionary<string, (string, string)>
            {
                { "FG-00001", ("FAKEGOOGLE", "FAKEPASSWORD1234") },
                { "FG-00002", ("FAKEPEOPLE", "FAKEPASSWORD4578") }
            };

        public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Ensure the action has a parameter "request" of type SubmitTrxMessageRequest.
            if (context.ActionArguments.TryGetValue("request", out var value) && value is SubmitTrxMessageRequest request)
            {
                if (!IsValidPartner(request))
                {
                    context.Result = new UnauthorizedObjectResult(new { message = "Access Denied!" });
                    return;
                }
            }
            else
            {
                context.Result = new BadRequestObjectResult(new { message = "Request body is missing or invalid." });
                return;
            }

            await next();
        }

        private bool IsValidPartner(SubmitTrxMessageRequest request)
        {
            // Check if the provided PartnerRefNo exists in allowed dictionary.
            if (!_allowedPartners.TryGetValue(request.PartnerRefNo, out var allowed))
            {
                return false;
            }

            // Validate that both the PartnerKey and PartnerPassword match the allowed values.
            return request.PartnerKey == allowed.AllowedPartner &&
                   request.PartnerPassword == Convert.ToBase64String(Encoding.UTF8.GetBytes(allowed.AllowedPassword));
        }
    }
}
