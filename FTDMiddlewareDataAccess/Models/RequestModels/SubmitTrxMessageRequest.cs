using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FTDMiddlewareDataAccess.Models.RequestModels;

public class SubmitTrxMessageRequest : Base
{
    [Required(ErrorMessage = "totalamount is required."), JsonPropertyName("totalamount")]
    public long TotalAmount { get; set; }

    [JsonPropertyName("items")]
    public List<Item> Items { get; set; } = [];

    public class Item
    {
        [JsonPropertyName("partneritemref")]
        public string PartnerItemRef { get; set; } = "";
        [JsonPropertyName("name")]
        public string Name { get; set; } = "";
        [JsonPropertyName("qty")]
        public int Qty { get; set; }
        [JsonPropertyName("unitprice")]
        public long UnitPrice { get; set; }
    }

    
}
