using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eatery_API.Domain.Entities
{
    public class PaymentMethod : BaseEntity
    {
        [Required]
        [Column(TypeName = "VARCHAR(20)")]
        public required string Name { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(10)")]
        public required string MethodCode { get; set; }

        [Required]
        [Column(TypeName = "BOOLEAN")]
        public bool IsActive { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = default!;
    }
}
