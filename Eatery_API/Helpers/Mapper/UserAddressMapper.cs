using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;

namespace Eatery_API.Helpers.Mapper
{
    public class UserAddressMapper
    {
        public UserAddress MapToEntity(DTOUserAddressCreate addressCreate)
        {
            return new UserAddress
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(addressCreate.UserId),
                Address = addressCreate.Address,
                PhoneNumber = addressCreate.PhoneNumber,
                IsDeleted = false
            };
        }

        public ResponseUserAddress MapToResponse(UserAddress? userAddress)
        {
            return new ResponseUserAddress
            {
                Id = userAddress?.Id.ToString(),
                UserId = userAddress?.UserId.ToString(),
                Address = userAddress?.Address,
                PhoneNumber = userAddress?.PhoneNumber,
                IsDeleted = userAddress?.IsDeleted ?? false
            };
        }
    }
}
