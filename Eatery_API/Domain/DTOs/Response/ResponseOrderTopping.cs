using System.Text.Json.Serialization;

namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseOrderTopping
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }
        [JsonPropertyName("OrderItemId")]
        public string? OrderItemId { get; set; }
        [JsonPropertyName("ToppingId")]
        public string? ToppingId { get; set; }
        [JsonPropertyName("Topping")]
        public ResponseTopping? Topping { get; set; }
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("UnitPrice")]
        public decimal UnitPrice { get; set; }
        [JsonPropertyName("CreateAt")]
        public DateTime CreateAt { get; set; }
        [JsonPropertyName("UpdateAt")]
        public DateTime UpdateAt { get; set; }
    }
}
