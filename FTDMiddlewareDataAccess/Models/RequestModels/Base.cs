using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FTDMiddlewareDataAccess.Models.RequestModels;

public class Base
{
    [Required(ErrorMessage = "partnerkey is required"), JsonPropertyName("partnerkey")]
    public string PartnerKey { get; set; }
    [Required(ErrorMessage = "partnerrefno is required"), JsonPropertyName("partnerrefno")]
    public string PartnerRefNo { get; set; }
    [Required(ErrorMessage = "partnerpassword is required"), JsonPropertyName("partnerpassword")]
    public string PartnerPassword { get; set; }
    [Required(ErrorMessage = "timestamp is required"), JsonPropertyName("timestamp")]
    public string timestamp {get; set;}
    [Required(ErrorMessage = "sig is required"), JsonPropertyName("sig")]
    public string sig {get; set;}

}
