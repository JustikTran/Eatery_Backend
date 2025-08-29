using System.ComponentModel.DataAnnotations;

namespace Eatery_API.Domain.DTOs.Request
{
    public class DTOCartToppingCreate
    {
        public string? CartId { get; set; }
        [Required]
        public required string ToppingId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
        public required int Quantity { get; set; }
    }

    public class DTOCartToppingUpdate : DTOCartToppingCreate
    {
        [Required]
        public required string Id { get; set; }
    }
}
