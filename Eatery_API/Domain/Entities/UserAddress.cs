using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eatery_API.Domain.Entities
{
    public class UserAddress
    {
        [Key]
        [Required]
        [Column(TypeName = "UUID")]
        public required Guid Id { get; set; }

        [Required]
        [Column(TypeName = "UUID")]
        public required Guid UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; } = default!;

        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public required string Address { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(15)")]
        public required string PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "BOOLEAN")]
        public required bool IsDeleted { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = default!;
    }
}
