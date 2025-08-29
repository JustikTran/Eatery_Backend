using System.ComponentModel.DataAnnotations;

namespace Eatery_API.Domain.DTOs.Request
{
    public class DTOOrderItemCreate
    {
        public string? OrderId { get; set; }

        [Required]
        public required string DishId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public required int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be a non-negative value.")]
        public required decimal UnitPrice { get; set; }

        public List<DTOOrderToppingCreate>? Toppings { get; set; }
    }

    public class DTOOrderItemUpdate : DTOOrderItemCreate
    {
        [Required]
        public required string Id { get; set; }
    }
}
