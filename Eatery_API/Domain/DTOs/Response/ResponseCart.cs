using System.Text.Json.Serialization;

namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseCart
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }
        [JsonPropertyName("UserId")]
        public string? UserId { get; set; }
        [JsonPropertyName("DishId")]
        public string? DishId { get; set; }
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("CartToppings")]
        public List<ResponseCartTopping>? CartToppings { get; set; }
    }
}
