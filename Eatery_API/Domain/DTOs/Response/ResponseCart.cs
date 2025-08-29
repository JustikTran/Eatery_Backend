namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseCart
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public string? DishId { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<ResponseCartTopping>? CartToppings { get; set; }
    }
}
