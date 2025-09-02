using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.Mapper;
using Eatery_API.Infrastructures.Data;

namespace Eatery_API.Infrastructures.Providers
{
    public class DishProvider : IDishProvider
    {
        private AppDbContext context;
        public DishProvider(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentException(nameof(context));
        }
        public async Task<Response> Create(DTODishCreate dish)
        {
            try
            {
                var dishEntity = PatternMapper<DishMapper>.Instance.MapToEntity(dish);
                context.Dishes.Add(dishEntity);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 201,
                        Message = "Dish created successfully",
                        Data = PatternMapper<DishMapper>.Instance.MapToResponse(dishEntity)
                    };
                }
                else
                {
                    throw new Exception("Failed to create dish");
                }
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }

        public async Task<Response> Delete(string id)
        {
            try
            {
                var existingDish = await context.Dishes.FindAsync(Guid.Parse(id));
                if (existingDish == null || existingDish.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Dish not found"
                    };
                }
                existingDish.IsDeleted = true;
                existingDish.UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                context.Dishes.Update(existingDish);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Dish deleted successfully"
                    };
                }
                else
                {
                    throw new Exception("Failed to delete dish");
                }
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }

        public IQueryable<ResponseDish> GetAll()
        {
            try
            {
                var listDishes = context.Dishes.ToList();
                return listDishes
                    .Select(PatternMapper<DishMapper>.Instance.MapToResponse)
                    .AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> GetById(string id)
        {
            try
            {
                var existingDish = await context.Dishes.FindAsync(Guid.Parse(id));
                if (existingDish == null || existingDish.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Dish not found"
                    };
                }
                return new Response
                {
                    StatusCode = 200,
                    Message = "Dish retrieved successfully",
                    Data = PatternMapper<DishMapper>.Instance.MapToResponse(existingDish)
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }

        public async Task<Response> Update(DTODishUpdate dish)
        {
            try
            {
                var existingDish = await context.Dishes.FindAsync(Guid.Parse(dish.Id));
                if (existingDish == null || existingDish.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Dish not found"
                    };
                }
                existingDish.Name = dish.Name;
                existingDish.Description = dish.Description;
                existingDish.Price = dish.Price;
                existingDish.Image = dish.Image;
                existingDish.InStock = dish.InStock;
                existingDish.UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                context.Dishes.Update(existingDish);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Dish updated successfully",
                        Data = PatternMapper<DishMapper>.Instance.MapToResponse(existingDish)
                    };
                }
                else
                {
                    throw new Exception("Failed to update dish");
                }
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }
    }
}
