using System.ComponentModel.DataAnnotations;

namespace Eatery_API.Domain.DTOs.Request
{
    public class DTOCartCreate
    {
        [Required]
        public required string UserId { get; set; }

        [Required]
        public required string DishId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public required int Quantity { get; set; }

        public List<DTOCartToppingCreate>? CartToppings { get; set; }
    }

    public class DTOCartUpdate : DTOCartCreate
    {
        [Required]
        public required string Id { get; set; }
    }
}
