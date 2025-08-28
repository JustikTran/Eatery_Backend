using System.ComponentModel.DataAnnotations;

namespace Eatery_API.Domain.DTOs.Request
{
    public class DTOUserAddressCreate
    {
        [Required]
        public required string UserId { get; set; }

        [Required]
        [StringLength(50)]
        public required string Address { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 10)]
        public required string PhoneNumber { get; set; }
    }

    public class DTOUserAddressUpdate : DTOUserAddressCreate
    {
        [Required]
        public required string Id { get; set; }

        public bool IsDeleted { get; set; }
    }
}
