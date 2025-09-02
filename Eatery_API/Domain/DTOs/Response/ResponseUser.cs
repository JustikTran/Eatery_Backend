using System.Text.Json.Serialization;

namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseUser
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }
        [JsonPropertyName("FirstName")]
        public string? FirstName { get; set; }
        [JsonPropertyName("LastName")]
        public string? LastName { get; set; }
        [JsonPropertyName("Avatar")]
        public string? Avatar { get; set; }
        [JsonPropertyName("DateOfBirth")]
        public DateOnly DateOfBirth { get; set; }
        [JsonPropertyName("Language")]
        public string? Language { get; set; }
        [JsonPropertyName("AccountId")]
        public string? AccountId { get; set; }
        [JsonPropertyName("IsDeleted")]
        public bool IsDeleted { get; set; }
        [JsonPropertyName("CreateAt")]
        public DateTime CreateAt { get; set; }
        [JsonPropertyName("UpdateAt")]
        public DateTime UpdateAt { get; set; }
    }
}
