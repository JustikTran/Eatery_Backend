using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;

namespace Eatery_API.Helpers.Mapper
{
    public class PaymentMethodMapper
    {
        public PaymentMethod MapToEntity(DTOPaymentMethodCreate methodCreate)
        {
            return new PaymentMethod
            {
                Id = Guid.NewGuid(),
                Name = methodCreate.Name,
                MethodCode = methodCreate.MethodCode,
                IsActive = true
            };
        }

        public ResponsePaymentMethod MapToResponse(PaymentMethod? method)
        {
            return new ResponsePaymentMethod
            {
                Id = method?.Id.ToString(),
                Name = method?.Name,
                MethodCode = method?.MethodCode,
                IsActive = method?.IsActive ?? false,
                CreateAt = method?.CreatedAt ?? DateTime.MinValue,
                UpdateAt = method?.UpdatedAt ?? DateTime.MinValue
            };
        }
    }
}
