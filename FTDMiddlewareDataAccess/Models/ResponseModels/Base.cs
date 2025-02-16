using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FTDMiddlewareDataAccess.Models.ResponseModels;

public class Base
{
    [Required(ErrorMessage = "result is required")]
    [JsonPropertyName("result")]
    public int Result {get; set;}

    [JsonPropertyName("resultmessage")]
    public string ResultMessage {get; set;}
}