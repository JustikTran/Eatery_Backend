using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.Mapper;
using Eatery_API.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;

namespace Eatery_API.Infrastructures.Providers
{
    public class OrderToppingProvider : IOrderToppingProvider
    {
        private AppDbContext _context;
        public OrderToppingProvider(AppDbContext context)
        {
            _context = context ?? throw new ArgumentException(nameof(context));
        }
        public async Task<Response> CreateOrderTopping(DTOOrderToppingCreate orderToppingCreate)
        {
            try
            {
                var orderTopping = PatternMapper<OrderToppingMapper>.Instance.MapToEntity(orderToppingCreate);
                _context.OrderToppings.Add(orderTopping);
                var result = await _context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        Message = "Order Topping created successfully",
                        StatusCode = 201,
                        Data = PatternMapper<OrderToppingMapper>.Instance.MapToResponse(orderTopping)
                    };
                }
                else
                {
                    throw new Exception("Failed to create Order Topping");
                }
            }
            catch (Exception err)
            {
                return new Response
                {
                    Message = err.Message,
                    StatusCode = 500,
                };
            }
        }

        public IQueryable<ResponseOrderTopping> GetAll()
        {
            try
            {
                var listOrderTopping = _context.OrderToppings
                    .Include(ot => ot.Topping)
                    .Include(ot => ot.OrderItem)
                    .ThenInclude(oi => oi.Order)
                    .ToList();
                return listOrderTopping
                    .Select(PatternMapper<OrderToppingMapper>.Instance.MapToResponse)
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
                var existing = await _context.OrderToppings
                    .Include(ot => ot.Topping)
                    .Include(ot => ot.OrderItem)
                        .ThenInclude(oi => oi.Order)
                    .FirstOrDefaultAsync(ot => ot.Id.ToString() == id);
                if (existing == null || existing.OrderItem.Order.IsDeleted)
                {
                    return new Response
                    {
                        Message = "Order Topping not found",
                        StatusCode = 404,
                    };
                }
                return new Response
                {
                    Message = "Order Topping found",
                    StatusCode = 200,
                    Data = PatternMapper<OrderToppingMapper>.Instance.MapToResponse(existing)
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    Message = err.Message,
                    StatusCode = 500,
                };
            }
        }
    }
}
