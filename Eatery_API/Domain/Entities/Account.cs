using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eatery_API.Domain.Entities
{
    public class Account : BaseEntity
    {
        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public required string Username { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(32)")]
        public required string Password { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(5)")]
        public required string Role { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(100)")]
        public required string Email { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(15)")]
        public required string PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "BOOLEAN")]
        public required bool IsActived { get; set; }

        [Required]
        [Column(TypeName = "BOOLEAN")]
        public required bool IsBanned { get; set; }

        [Required]
        [Column(TypeName = "BOOLEAN")]
        public required bool IsDeleted { get; set; }

        public virtual User User { get; set; } = default!;
    }
}
