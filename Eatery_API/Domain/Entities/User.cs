using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata.Ecma335;

namespace Eatery_API.Domain.Entities
{
    public class User : BaseEntity
    {
        [Required]
        [Column(TypeName = "VARCHAR(20)")]
        public required string FirstName { get; set; }
        [Required]
        [Column(TypeName = "VARCHAR(20)")]
        public required string LastName { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public required string Avatar { get; set; }

        [Required]
        [Column(TypeName = "DATE")]
        public required DateOnly DateOfBirth { get; set; }

        [Required]
        [Column(TypeName = "VARCHAR(5)")]
        public required string Language { get; set; }

        [Required]
        [Column(TypeName = "BOOLEAN")]
        public required bool IsDeleted { get; set; }

        [Required]
        [Column(TypeName = "UUID")]
        public required Guid AccountId { get; set; }

        [ForeignKey(nameof(AccountId))]
        public virtual Account Account { get; set; } = default!;

        public virtual ICollection<UserAddress> UserAddresses { get; set; } = default!;
        public virtual ICollection<Cart> Carts { get; set; } = default!;
    }
}
