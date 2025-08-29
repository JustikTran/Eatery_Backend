namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseCartTopping
    {
        public string? Id { get; set; }
        public string? CartId { get; set; }
        public string? ToppingId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
