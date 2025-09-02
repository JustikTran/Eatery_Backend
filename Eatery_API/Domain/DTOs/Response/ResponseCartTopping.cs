using System.Text.Json.Serialization;

namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseCartTopping
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }
        [JsonPropertyName("CartId")]
        public string? CartId { get; set; }
        [JsonPropertyName("ToppingId")]
        public string? ToppingId { get; set; }
        [JsonPropertyName("Quantity")]
        public int Quantity { get; set; }
        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
