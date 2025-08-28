using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.Mapper;
using Eatery_API.Infrastructures.Data;

namespace Eatery_API.Infrastructures.Providers
{
    public class ToppingProvider : IToppingProvider
    {
        private AppDbContext context;
        public ToppingProvider(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentException(nameof(context));
        }
        public async Task<Response> Create(DTOToppingCreate toppingCreate)
        {
            try
            {
                var topping = PatternMapper<ToppingMapper>.Instance.MapToEntity(toppingCreate);
                context.Toppings.Add(topping);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 201,
                        Message = "Topping created successfully",
                        Data = PatternMapper<ToppingMapper>.Instance.MapToResponse(topping)
                    };
                }
                else
                {
                    throw new Exception("Failed to create topping");
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
                var existingTopping = await context.Toppings.FindAsync(Guid.Parse(id));
                if (existingTopping == null || existingTopping.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Topping not found"
                    };
                }
                existingTopping.IsDeleted = true;
                context.Toppings.Update(existingTopping);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Topping deleted successfully"
                    };
                }
                else
                {
                    throw new Exception("Failed to delete topping");
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

        public IQueryable<ResponseTopping> GetAll()
        {
            try
            {
                var listTopping = context.Toppings.ToList();
                return listTopping
                    .Select(PatternMapper<ToppingMapper>.Instance.MapToResponse)
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
                var existingTopping = await context.Toppings.FindAsync(Guid.Parse(id));
                if (existingTopping == null || existingTopping.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Topping not found"
                    };
                }
                return new Response
                {
                    StatusCode = 200,
                    Message = "Topping retrieved successfully",
                    Data = PatternMapper<ToppingMapper>.Instance.MapToResponse(existingTopping)
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

        public async Task<Response> Update(DTOToppingUpdate toppingUpdate)
        {
            try
            {
                var existingTopping = await context.Toppings.FindAsync(Guid.Parse(toppingUpdate.Id));
                if (existingTopping == null || existingTopping.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Topping not found"
                    };
                }
                existingTopping.Name = toppingUpdate.Name;
                existingTopping.Description = toppingUpdate.Description;
                existingTopping.Image = toppingUpdate.Image;
                existingTopping.Price = toppingUpdate.Price;
                existingTopping.InStock = toppingUpdate.InStock;
                context.Toppings.Update(existingTopping);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Topping updated successfully",
                        Data = PatternMapper<ToppingMapper>.Instance.MapToResponse(existingTopping)
                    };
                }
                else
                {
                    throw new Exception("Failed to update topping");
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
