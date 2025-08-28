using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eatery_API.Domain.DTOs.Request
{
    public class DTOToppingCreate
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string Name { get; set; }

        [Required]
        public required string Description { get; set; }

        [Required]
        public required string Image { get; set; }

        [Required]
        [Range(1000, 100000)]
        public required decimal Price { get; set; }
    }

    public class DTOToppingUpdate : DTOToppingCreate
    {
        [Required]
        public required string Id { get; set; }

        [DefaultValue(false)]
        public bool InStock { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
