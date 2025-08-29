using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eatery_API.Domain.Entities
{
    public class OrderTopping : BaseEntity
    {
        [Required]
        [Column(TypeName = "UUID")]
        public required Guid OrderItemId { get; set; }

        [ForeignKey(nameof(OrderItemId))]
        public virtual OrderItem OrderItem { get; set; } = default!;

        [Required]
        [Column(TypeName = "UUID")]
        public required Guid ToppingId { get; set; }

        [ForeignKey(nameof(ToppingId))]
        public virtual Topping Topping { get; set; } = default!;

        [Required]
        [Column(TypeName = "INTEGER")]
        public required int Quantity { get; set; }

        [Required]
        [Column(TypeName = "MONEY")]
        public required decimal UnitPrice { get; set; }
    }
}
