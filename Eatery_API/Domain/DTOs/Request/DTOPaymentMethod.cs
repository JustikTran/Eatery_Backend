using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Eatery_API.Domain.DTOs.Request
{
    public class DTOPaymentMethodCreate
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public required string Name { get; set; }
        [Required]
        [StringLength(10, MinimumLength = 2)]
        public required string MethodCode { get; set; }
    }

    public class DTOPaymentMethodUpdate : DTOPaymentMethodCreate
    {
        [Required]
        public required string Id { get; set; }

        [DefaultValue(true)]
        public bool IsActive { get; set; }
    }
}
