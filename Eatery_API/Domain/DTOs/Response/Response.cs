using System.Text.Json.Serialization;

namespace Eatery_API.Domain.DTOs.Response
{
    public class Response
    {
        [JsonPropertyName("statusCode")]
        public int StatusCode { get; set; }
        [JsonPropertyName("message")]
        public string? Message { get; set; }
        [JsonPropertyName("data")]
        public object? Data { get; set; }
    }
}
