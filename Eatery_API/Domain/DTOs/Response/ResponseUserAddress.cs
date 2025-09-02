using System.Text.Json.Serialization;

namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseUserAddress
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }
        [JsonPropertyName("UserId")]
        public string? UserId { get; set; }
        [JsonPropertyName("Address")]
        public string? Address { get; set; }
        [JsonPropertyName("PhoneNumber")]
        public string? PhoneNumber { get; set; }
        [JsonPropertyName("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
