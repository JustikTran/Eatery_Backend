using System.Text.Json.Serialization;

namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseOrderItem
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }
        [JsonPropertyName("OrderId")]
        public string? OrderId { get; set; }
        [JsonPropertyName("DishId")]
        public string? DishId { get; set; }
        [JsonPropertyName("Dish")]
        public ResponseDish? Dish { get; set; }
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("UnitPrice")]
        public decimal UnitPrice { get; set; }
        [JsonPropertyName("TotalPrice")]
        public List<ResponseOrderTopping>? OrderToppings { get; set; }
        [JsonPropertyName("TotalPrice")]
        public DateTime CreateAt { get; set; }
        [JsonPropertyName("UpdateAt")]
        public DateTime UpdateAt { get; set; }
    }
}
