using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FTDMiddlewareDataAccess.Models.ResponseModels;

public class SubmitTrxMessageResponse : Base
{
    [JsonPropertyName("totalamount")]
    public long TotalAmount { get; set; }

    [JsonPropertyName("totaldiscount")]
    public long TotalDiscount { get; set; }

    [JsonPropertyName("finalamount")]
    public long FinalAmount { get; set; }
}