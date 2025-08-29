namespace Eatery_API.Domain.DTOs.Response
{
    public class ResponseOrderItem
    {
        public string? Id { get; set; }
        public string? OrderId { get; set; }
        public string? DishId { get; set; }
        public ResponseDish? Dish { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public List<ResponseOrderTopping>? OrderToppings { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
