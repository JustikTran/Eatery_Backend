using System.ComponentModel.DataAnnotations;

namespace Eatery_API.Domain.DTOs.Request
{
    public class DTOUserCreate
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 2)]
        public required string LastName { get; set; }

        [Required]
        public required string Avatar { get; set; }

        [Required]
        public required DateOnly DateOfBirth { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 2)]
        public required string Language { get; set; }

        [Required]
        public required string AccountId { get; set; }

        public bool IsDeleted { get; set; } = false;
    }

    public class DTOUserUpdate : DTOUserCreate
    {
        [Required]
        public required string Id { get; set; }
    }
}
