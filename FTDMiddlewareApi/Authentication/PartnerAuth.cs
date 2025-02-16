using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;

namespace FTDMiddlewareApi.Authentication
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PartnerAuthAttribute : Attribute, IAuthorizationFilter
    {
        // Simulate a database with allowed partners
        private static readonly Dictionary<string, Dictionary<string, string>> AllowedPartners = new()
        {
            { "FG-0001", new Dictionary<string, string> 
                { 
                    { "PartnerName", "FAKEGOOGLE" },
                    { "Password", "RkFLRVBBU1NXT1JEMTIzNA==" }  // Base64-encoded password
                } 
            }
        };

//         public async Task<bool> ValidatePartner(HttpContext context)
// {
//     using var reader = new StreamReader(context.Request.Body);
//     var body = await reader.ReadToEndAsync();

//     if (string.IsNullOrEmpty(body))
//         return false; // Body is empty

//     var requestData = JsonSerializer.Deserialize<PartnerRequest>(body, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

//     if (requestData == null || string.IsNullOrEmpty(requestData.PartnerNo) || string.IsNullOrEmpty(requestData.PartnerPassword))
//         return false; // Invalid request

//     string partnerNo = requestData.PartnerNo;
//     string partnerPassword = requestData.PartnerPassword;

//     Console.WriteLine($"Partner No: {partnerNo}, Partner Password: {partnerPassword}");
//     return true;
// }


        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var request = context.HttpContext.Request;

            // Check if the request contains "partner-no" and "partner-password" headers
            if (!request.Headers.TryGetValue("partner-no", out var partnerNo) ||
                !request.Headers.TryGetValue("partner-password", out var partnerPassword))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Validate credentials
            if (!AllowedPartners.TryGetValue(partnerNo, out var storedPassword) || storedPassword != partnerPassword)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}
