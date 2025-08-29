namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseOrderTopping
    {
        public string? Id { get; set; }
        public string? OrderItemId { get; set; }
        public string? ToppingId { get; set; }
        public ResponseTopping? Topping { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
