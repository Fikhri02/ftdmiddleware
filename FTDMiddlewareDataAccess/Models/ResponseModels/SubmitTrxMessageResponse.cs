using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FTDMiddlewareDataAccess.Models.ResponseModels;

public class SubmitTrxMessageResponse : Base
{
    [JsonPropertyName("totalamount")]
    [Range(1, double.MaxValue, ErrorMessage = "Unit Price must be a positive value.")]
    public long? TotalAmount { get; set; } = null;

    [JsonPropertyName("totaldiscount")]
    [Range(1, double.MaxValue, ErrorMessage = "Total Discount must be a positive value.")]
    public long? TotalDiscount { get; set; } = null;

    [JsonPropertyName("finalamount")]
    [Range(1, double.MaxValue, ErrorMessage = "Final Amount must be a positive value.")]
    public long? FinalAmount { get; set; } = null;
}