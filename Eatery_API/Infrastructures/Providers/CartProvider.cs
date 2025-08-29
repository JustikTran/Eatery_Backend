using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.Mapper;
using Eatery_API.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;

namespace Eatery_API.Infrastructures.Providers
{
    public class CartProvider : ICartProvider
    {
        private AppDbContext context;
        public CartProvider(AppDbContext _context)
        {
            context = _context ?? throw new ArgumentException(nameof(_context));
        }

        public async Task<Response> CreateCart(DTOCartCreate cartCreate)
        {
            try
            {
                var cart = PatternMapper<CartMapper>.Instance.MapToEntity(cartCreate);
                context.Carts.Add(cart);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 201,
                        Message = "Cart created successfully",
                        Data = PatternMapper<CartMapper>.Instance.MapToResponse(cart)
                    };
                }
                else
                {
                    throw new Exception("Failed to create cart");
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

        public async Task<Response> DeleteCart(string id)
        {
            try
            {
                var existingCart = await context.Carts.FindAsync(Guid.Parse(id));
                if (existingCart == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Cart not found"
                    };
                }
                context.Carts.Remove(existingCart);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Cart deleted successfully"
                    };
                }
                else
                {
                    throw new Exception("Failed to delete cart");
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
                var existingCart = await context.Carts
                    .Include(c => c.CartToppings)
                    .FirstOrDefaultAsync(c => c.Id == Guid.Parse(id));
                if (existingCart == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Cart not found"
                    };
                }
                return new Response
                {
                    StatusCode = 200,
                    Message = "Cart retrieved successfully",
                    Data = PatternMapper<CartMapper>.Instance.MapToResponse(existingCart)
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

        public async Task<Response> GetByUserId(string userId)
        {
            try
            {
                var existingCart = await context.Carts
                    .Include(c => c.CartToppings)
                    .FirstOrDefaultAsync(c => c.UserId == Guid.Parse(userId));
                if (existingCart == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Cart not found"
                    };
                }
                return new Response
                {
                    StatusCode = 200,
                    Message = "Cart retrieved successfully",
                    Data = PatternMapper<CartMapper>.Instance.MapToResponse(existingCart)
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

        public IQueryable<ResponseCart> GetCarts()
        {
            try
            {
                var listCarts = context.Carts
                    .Include(c => c.CartToppings)
                    .OrderBy(c => c.CreatedAt)
                    .ToList();
                return listCarts
                    .Select(PatternMapper<CartMapper>.Instance.MapToResponse)
                    .AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> UpdateCart(DTOCartUpdate cartUpdate)
        {
            try
            {
                var existingCart = await context.Carts
                    .Include(c => c.CartToppings)
                    .FirstOrDefaultAsync(c => c.Id.ToString() == cartUpdate.Id);
                if (existingCart == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Cart not found"
                    };
                }
                existingCart.Quantity = cartUpdate.Quantity;
                existingCart.UpdatedAt = DateTime.Now;
                context.Carts.Update(existingCart);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Cart updated successfully",
                        Data = PatternMapper<CartMapper>.Instance.MapToResponse(existingCart)
                    };
                }
                else
                {
                    throw new Exception("Failed to update cart");
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
