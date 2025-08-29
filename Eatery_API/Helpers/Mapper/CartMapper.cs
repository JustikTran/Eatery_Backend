using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;

namespace Eatery_API.Helpers.Mapper
{
    public class CartMapper
    {
        public Cart MapToEntity(DTOCartCreate cartCreate)
        {
            return new Cart
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(cartCreate.UserId),
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                DishId = Guid.Parse(cartCreate.DishId),
                Quantity = cartCreate.Quantity,
                CartToppings = cartCreate.CartToppings?
                .Select(PatternMapper<CartToppingMapper>.Instance.MapToEntity)
                .ToList() ?? new List<CartTopping>(),
            };
        }

        public ResponseCart MapToResponse(Cart? cart)
        {
            return new ResponseCart
            {
                Id = cart?.Id.ToString() ?? string.Empty,
                UserId = cart?.UserId.ToString() ?? string.Empty,
                DishId = cart?.DishId.ToString() ?? string.Empty,
                Quantity = cart?.Quantity ?? 0,
                CreatedAt = cart?.CreatedAt ?? DateTime.MinValue,
                UpdatedAt = cart?.UpdatedAt ?? DateTime.MinValue,
                CartToppings = cart?.CartToppings?
                .Select(PatternMapper<CartToppingMapper>.Instance.MapToResponse)
                .ToList() ?? new List<ResponseCartTopping>(),
            };
        }
    }
}
