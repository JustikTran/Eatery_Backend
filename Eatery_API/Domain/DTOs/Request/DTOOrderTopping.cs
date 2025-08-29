using System.ComponentModel.DataAnnotations;

namespace Eatery_API.Domain.DTOs.Request
{
    public class DTOOrderToppingCreate
    {
        public string? OrderItemId { get; set; }

        [Required]
        public required string ToppingId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public required int Quantity { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Unit price must be a non-negative value.")]
        public required decimal UnitPrice { get; set; }
    }

    public class DTOOrderToppingUpdate : DTOOrderToppingCreate
    {
        [Required]
        public required string Id { get; set; }
    }
}
