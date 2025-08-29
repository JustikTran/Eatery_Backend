namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseOrder
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public ResponseUser? User { get; set; }
        public string? Status { get; set; }
        public decimal TotalPrice { get; set; }
        public string? AddressId { get; set; }
        public ResponseUserAddress? Address { get; set; }
        public bool Paid { get; set; }
        public DateTime PaidAt { get; set; }
        public string? PaymentMethodId { get; set; }
        //public ResponsePaymentMethod? PaymentMethod { get; set; }
        public List<ResponseOrderItem>? OrderItems { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
