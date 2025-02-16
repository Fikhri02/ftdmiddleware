using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using FTDMiddlewareDataAccess.Attributes;

namespace FTDMiddlewareDataAccess.Models.RequestModels;

public class Base
{
    [JsonPropertyName("partnerkey")]
    [Required(ErrorMessage = "partnerkey is required")]
    public string PartnerKey { get; set; }

    [JsonPropertyName("partnerrefno")]
    [Required(ErrorMessage = "partnerrefno is required")]
    public string PartnerRefNo { get; set; }

    [JsonPropertyName("partnerpassword")]
    [Required(ErrorMessage = "partnerpassword is required")]
    public string PartnerPassword { get; set; }

    [JsonPropertyName("timestamp")]
    // [Required(ErrorMessage = "timestamp is required")]
    // [Iso8601Utc(ErrorMessage = "Timestamp must be in ISO 8601 format with UTC 'Z' (e.g., 2024-08-15T02:11:22.0000000Z).")]
    [ValidTimestamp]
    public string Timestamp {get; set;}

 

}
