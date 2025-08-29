using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eatery_API.Domain.Entities
{
    public class CartTopping : BaseEntity
    {
        [Required]
        [Column(TypeName = "UUID")]
        public required Guid CartId { get; set; }

        [ForeignKey(nameof(CartId))]
        public virtual Cart Cart { get; set; } = default!;

        [Required]
        [Column(TypeName = "UUID")]
        public required Guid ToppingId { get; set; }

        [ForeignKey(nameof(ToppingId))]
        public virtual Topping Topping { get; set; } = default!;

        [Required]
        [Column(TypeName = "INTEGER")]
        public required int Quantity { get; set; }
    }
}
