using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.Mapper;
using Eatery_API.Infrastructures.Data;

namespace Eatery_API.Infrastructures.Providers
{
    public class CartToppingProvider : ICartToppingProvider
    {
        private AppDbContext context;
        public CartToppingProvider(AppDbContext _context)
        {
            context = _context ?? throw new ArgumentException(nameof(_context));
        }
        public async Task<Response> CreateCartTopping(DTOCartToppingCreate cartToppingCreate)
        {
            try
            {
                var cartTopping = PatternMapper<CartToppingMapper>.Instance.MapToEntity(cartToppingCreate);
                context.CartToppings.Add(cartTopping);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 201,
                        Message = "Cart Topping created successfully",
                        Data = PatternMapper<CartToppingMapper>.Instance.MapToResponse(cartTopping)
                    };
                }
                else
                {
                    throw new Exception("Failed to create cart topping");
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

        public async Task<Response> DeleteCartTopping(string id)
        {
            try
            {
                var existingCartTopping = await context.CartToppings.FindAsync(Guid.Parse(id));
                if (existingCartTopping == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Cart Topping not found"
                    };
                }
                context.CartToppings.Remove(existingCartTopping);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Cart Topping deleted successfully"
                    };
                }
                else
                {
                    throw new Exception("Failed to delete cart topping");
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

        public async Task<Response> GetById(string id)
        {
            try
            {
                var existingCartTopping = await context.CartToppings.FindAsync(Guid.Parse(id));
                if (existingCartTopping == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Cart Topping not found"
                    };
                }
                return new Response
                {
                    StatusCode = 200,
                    Message = "Cart Topping found",
                    Data = PatternMapper<CartToppingMapper>.Instance.MapToResponse(existingCartTopping)
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

        public IQueryable<ResponseCartTopping> GetCartToppings()
        {
            try
            {
                var listCartTopping = context.CartToppings.ToList();
                return listCartTopping
                    .Select(PatternMapper<CartToppingMapper>.Instance.MapToResponse)
                    .AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> UpdateCartTopping(DTOCartToppingUpdate cartToppingUpdate)
        {
            try
            {
                var existingCartTopping = await context.CartToppings.FindAsync(Guid.Parse(cartToppingUpdate.Id));
                if (existingCartTopping == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Cart Topping not found"
                    };
                }
                existingCartTopping.Quantity = cartToppingUpdate.Quantity;
                existingCartTopping.UpdatedAt = DateTime.Now;

                context.CartToppings.Update(existingCartTopping);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Cart Topping updated successfully",
                        Data = PatternMapper<CartToppingMapper>.Instance.MapToResponse(existingCartTopping)
                    };
                }
                else
                {
                    throw new Exception("Failed to update cart topping");
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
