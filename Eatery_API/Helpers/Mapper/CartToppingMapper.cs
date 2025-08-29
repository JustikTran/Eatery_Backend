using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;

namespace Eatery_API.Helpers.Mapper
{
    public class CartToppingMapper
    {
        public CartTopping MapToEntity(DTOCartToppingCreate cartToppingCreate)
        {
            return new CartTopping
            {
                Id = Guid.NewGuid(),
                CartId = cartToppingCreate.CartId != null ? Guid.Parse(cartToppingCreate.CartId) : Guid.Empty,
                ToppingId = Guid.Parse(cartToppingCreate.ToppingId),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                Quantity = cartToppingCreate.Quantity,
            };
        }

        public ResponseCartTopping MapToResponse(CartTopping? cartTopping)
        {
            return new ResponseCartTopping
            {
                Id = cartTopping?.Id.ToString() ?? string.Empty,
                CartId = cartTopping?.CartId.ToString() ?? string.Empty,
                ToppingId = cartTopping?.ToppingId.ToString() ?? string.Empty,
                Quantity = cartTopping?.Quantity ?? 0,
                CreatedAt = cartTopping?.CreatedAt ?? DateTime.MinValue,
                UpdatedAt = cartTopping?.UpdatedAt ?? DateTime.MinValue,
            };
        }
    }
}
