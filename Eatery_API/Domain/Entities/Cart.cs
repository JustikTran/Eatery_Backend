using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eatery_API.Domain.Entities
{
    public class Cart : BaseEntity
    {
        [Required]
        [Column(TypeName = "UUID")]
        public required Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = default!;

        [Required]
        [Column(TypeName = "UUID")]
        public required Guid DishId { get; set; }

        [ForeignKey(nameof(DishId))]
        public virtual Dish Dish { get; set; } = default!;

        [Required]
        [Column(TypeName = "INTEGER")]
        public required int Quantity { get; set; }

        public virtual ICollection<CartTopping> CartToppings { get; set; } = default!;
    }
}
