using System.Text.Json.Serialization;

namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponsePaymentMethod
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }
        [JsonPropertyName("Name")]
        public string? Name { get; set; }
        [JsonPropertyName("MethodCode")]
        public string? MethodCode { get; set; }
        [JsonPropertyName("IsActive")]
        public bool IsActive { get; set; }
        [JsonPropertyName("CreateAt")]
        public DateTime CreateAt { get; set; }
        [JsonPropertyName("UpdateAt")]
        public DateTime UpdateAt { get; set; }
    }
}
