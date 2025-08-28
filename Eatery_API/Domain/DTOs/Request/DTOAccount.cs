using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eatery_API.Domain.DTOs.Request
{
    public class DTOAccountCreate
    {
        [Required]
        [StringLength(50, MinimumLength = 6)]
        public required string Username { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public required string Password { get; set; }

        [DefaultValue("USER")]
        public string? Role { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(15, MinimumLength = 10)]
        public required string PhoneNumber { get; set; }
    }

    public class DTOAccountUpdate
    {
        [Required]
        [StringLength(32)]
        public required string Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 6)]
        public required string Username { get; set; }

        [DefaultValue("USER")]
        public string? Role { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(15, MinimumLength = 10)]
        public required string PhoneNumber { get; set; }

        [DefaultValue(false)]
        public bool IsAvtived { get; set; }

        [DefaultValue(false)]
        public bool IsBanned { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }

    public class DTOAccountChangePassword
    {
        [Required]
        [StringLength(32)]
        public required string Id { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public required string Email { get; set; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public required string Password { get; set; }
    }
}
