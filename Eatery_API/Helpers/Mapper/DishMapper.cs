using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;

namespace Eatery_API.Helpers.Mapper
{
    public class DishMapper
    {
        public Dish MapToEntity(DTODishCreate dishCreate)
        {
            return new Dish
            {
                Id = Guid.NewGuid(),
                Name = dishCreate.Name,
                Description = dishCreate.Description,
                Price = dishCreate.Price,
                Image = dishCreate.Image,
                InStock = true,
                IsDeleted = false,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
        }

        public ResponseDish MapToResponse(Dish? dish)
        {
            return new ResponseDish
            {
                Id = dish?.Id.ToString(),
                Name = dish?.Name,
                Description = dish?.Description,
                Price = dish?.Price ?? 0,
                Image = dish?.Image,
                InStock = dish?.InStock ?? false,
                IsDeleted = dish?.IsDeleted ?? false,
                CreateAt = dish?.CreatedAt ?? DateTime.MinValue,
                UpdateAt = dish?.UpdatedAt ?? DateTime.MinValue
            };
        }
    }
}
