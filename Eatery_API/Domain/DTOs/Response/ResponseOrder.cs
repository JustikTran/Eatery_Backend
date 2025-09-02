using System.Text.Json.Serialization;

namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseOrder
    {
        [JsonPropertyName("Id")]
        public string? Id { get; set; }
        [JsonPropertyName("UserId")]
        public string? UserId { get; set; }
        [JsonPropertyName("User")]
        public ResponseUser? User { get; set; }
        [JsonPropertyName("Status")]
        public string? Status { get; set; }
        [JsonPropertyName("TotalPrice")]
        public decimal TotalPrice { get; set; }
        [JsonPropertyName("AddressId")]
        public string? AddressId { get; set; }
        [JsonPropertyName("Address")]
        public ResponseUserAddress? Address { get; set; }
        [JsonPropertyName("Paid")]
        public bool Paid { get; set; }
        [JsonPropertyName("PaidAt")]
        public DateTime PaidAt { get; set; }
        [JsonPropertyName("PaymentMethodId")]
        public string? PaymentMethodId { get; set; }
        //public ResponsePaymentMethod? PaymentMethod { get; set; }
        [JsonPropertyName("OrderItems")]
        public List<ResponseOrderItem>? OrderItems { get; set; }
        [JsonPropertyName("CreateAt")]
        public DateTime CreateAt { get; set; }
        [JsonPropertyName("UpdateAt")]
        public DateTime UpdateAt { get; set; }
    }
}
