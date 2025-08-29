using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eatery_API.Domain.Entities
{
    public class OrderItem : BaseEntity
    {
        [Required]
        [Column(TypeName = "UUID")]
        public required Guid OrderId { get; set; }

        [ForeignKey(nameof(OrderId))]
        public virtual Order Order { get; set; } = default!;

        [Required]
        [Column(TypeName = "UUID")]
        public required Guid DishId { get; set; }

        [ForeignKey(nameof(DishId))]
        public virtual Dish Dish { get; set; } = default!;

        [Required]
        [Column(TypeName = "INTEGER")]
        public required int Quantity { get; set; }

        [Required]
        [Column(TypeName = "MONEY")]
        public required decimal UnitPrice { get; set; }

        public virtual List<OrderTopping> OrderToppings { get; set; } = default!;
    }
}
