using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;

namespace Eatery_API.Helpers.Mapper
{
    public class ToppingMapper
    {
        public Topping MapToEntity(DTOToppingCreate toppingCreate)
        {
            return new Topping
            {
                Id = Guid.NewGuid(),
                Name = toppingCreate.Name,
                Description = toppingCreate.Description,
                Image = toppingCreate.Image,
                Price = toppingCreate.Price,
                InStock = true,
                IsDeleted = false,
            };
        }

        public ResponseTopping MapToResponse(Topping? topping)
        {
            return new ResponseTopping
            {
                Id = topping?.Id.ToString(),
                Name = topping?.Name,
                Description = topping?.Description,
                Image = topping?.Image,
                Price = topping?.Price ?? 0,
                InStock = topping?.InStock ?? false,
                IsDeleted = topping?.IsDeleted ?? false,
                CreatedAt = topping?.CreatedAt ?? DateTime.MinValue,
                UpdatedAt = topping?.UpdatedAt ?? DateTime.MinValue
            };
        }
    }
}
