using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FTDMiddlewareDataAccess.Models.RequestModels;

public class SubmitTrxMessageRequest : Base
{
    [JsonPropertyName("totalamount")]
    [Required(ErrorMessage = "totalamount is required.")]
    [Range(1, double.MaxValue, ErrorMessage = "TotalAmount must be greater than 0.")]
    public long TotalAmount { get; set; }


    [JsonPropertyName("sig")]
    [Required(ErrorMessage = "sig is required")]
    [SignatureValidation]
    public string sig {get; set;}

    [JsonPropertyName("items")]
    public List<Item> Items { get; set; } = [];

    public class Item
    {
        [JsonPropertyName("partneritemref")]
        [MinLength(1, ErrorMessage = "Reference ID cannot be empty.")]
        public string PartnerItemRef { get; set; } = "";

        [JsonPropertyName("name")]
        [MinLength(1, ErrorMessage = "Name cannot be empty.")]
        public string Name { get; set; } = "";
        
        [JsonPropertyName("qty")]
        [Range(1, 5, ErrorMessage = "Quantity must be greater than 0 and lesser than 5.")]
        public int Qty { get; set; }
        
        [JsonPropertyName("unitprice")]
        [Range(1, double.MaxValue, ErrorMessage = "Unit Price must be a positive value.")]
        public long UnitPrice { get; set; }

    }

    
}
