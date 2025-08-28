using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Eatery_API.Domain.Entities
{
    public class Dish : BaseEntity
    {
        [Required]
        [Column(TypeName = "VARCHAR(50)")]
        public required string Name { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public required string Description { get; set; }

        [Required]
        [Column(TypeName = "MONEY")]
        public required decimal Price { get; set; }

        [Required]
        [Column(TypeName = "TEXT")]
        public required string Image { get; set; }

        [Required]
        [Column(TypeName = "BOOLEAN")]
        public required bool InStock { get; set; }

        [Required]
        [Column(TypeName = "BOOLEAN")]
        public required bool IsDeleted { get; set; }
    }
}
