using System.Text.Json.Serialization;

namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseTopping
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
        [JsonPropertyName("Description")]
        public string? Description { get; set; }
        [JsonPropertyName("Image")]
        public string? Image { get; set; }
        [JsonPropertyName("Price")]
        public decimal Price { get; set; }
        [JsonPropertyName("InStock")]
        public bool InStock { get; set; }
        [JsonPropertyName("IsDeleted")]
        public bool IsDeleted { get; set; }
        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }
    }
}
