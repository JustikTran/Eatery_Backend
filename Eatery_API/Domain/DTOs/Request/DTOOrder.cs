using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eatery_API.Domain.DTOs.Request
{
    public class DTOOrderCreate
    {
        [Required]
        public required string UserId { get; set; }

        [Required]
        [DefaultValue("Pending")]
        public required string Status { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a non-negative value.")]
        public required decimal TotalPrice { get; set; }

        [Required]
        public required string AddressId { get; set; }

        [DefaultValue(false)]
        public bool Paid { get; set; }

        public DateTime PaidAt { get; set; } = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Unspecified);

        [Required]
        public required string PaymentMethodId { get; set; }

        [Required]
        public required List<DTOOrderItemCreate> OrderItems { get; set; }
    }

    public class DTOOrderUpdate
    {
        [Required]
        public required string Id { get; set; }

        [Required]
        public required string UserId { get; set; }

        [Required]
        [DefaultValue("Pending")]
        public required string Status { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Total price must be a non-negative value.")]
        public required decimal TotalPrice { get; set; }

        [Required]
        public required string AddressId { get; set; }

        [DefaultValue(false)]
        public bool Paid { get; set; }

        public DateTime PaidAt { get; set; } = DateTime.SpecifyKind(DateTime.MinValue, DateTimeKind.Unspecified);

        [Required]
        public required string PaymentMethodId { get; set; }

        public bool IsDeleted { get; set; }
    }
}
