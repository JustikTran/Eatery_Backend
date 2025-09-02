using System.Text.Json.Serialization;

namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseAccount
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }
        [JsonPropertyName("Username")]
        public string? Username { get; set; }
        [JsonPropertyName("Email")]
        public string? Email { get; set; }
        [JsonPropertyName("PhoneNumber")]
        public string? PhoneNumber { get; set; }
        [JsonPropertyName("Role")]
        public string? Role { get; set; }
        [JsonPropertyName("CreatedAt")]
        public DateTime CreatedAt { get; set; }
        [JsonPropertyName("UpdatedAt")]
        public DateTime UpdatedAt { get; set; }
        [JsonPropertyName("IsActived")]
        public bool IsActived { get; set; }
        [JsonPropertyName("IsBanned")]
        public bool IsBanned { get; set; }
        [JsonPropertyName("IsDeleted")]
        public bool IsDeleted { get; set; }
    }
}
