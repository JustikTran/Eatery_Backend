using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eatery_API.Domain.Entities
{
    public class Order : BaseEntity
    {
        [Required]
        [Column(TypeName = "UUID")]
        public required Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = default!;

        [Required]
        [Column(TypeName = "VARCHAR(10)")]
        public required string Status { get; set; }

        [Required]
        [Column(TypeName = "MONEY")]
        public required decimal TotalPrice { get; set; }

        [Required]
        [Column(TypeName = "UUID")]
        public required Guid AddressId { get; set; }

        [ForeignKey(nameof(AddressId))]
        public virtual UserAddress Address { get; set; } = default!;

        [Required]
        [Column(TypeName = "BOOLEAN")]
        public bool Paid { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime? PaidAt { get; set; }

        [Required]
        [Column(TypeName = "BOOLEAN")]
        public bool IsDeleted { get; set; }

        [Required]
        [Column(TypeName = "UUID")]
        public required Guid PaymentMethodId { get; set; }

        [ForeignKey(nameof(PaymentMethodId))]
        public virtual PaymentMethod PaymentMethod { get; set; } = default!;

        public virtual List<OrderItem> OrderItems { get; set; } = default!;
    }
}
