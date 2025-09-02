using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.Mapper;
using Eatery_API.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;

namespace Eatery_API.Infrastructures.Providers
{
    public class OrderItemProvider : IOrderItemProvider
    {
        private AppDbContext context;
        public OrderItemProvider(AppDbContext _context)
        {
            context = _context ?? throw new ArgumentException(nameof(_context));
        }
        public async Task<Response> Create(DTOOrderItemCreate orderItemCreate)
        {
            try
            {
                var orderItem = PatternMapper<OrderItemMapper>.Instance.MapToEntity(orderItemCreate);
                context.OrderItems.Add(orderItem);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    var responseOrderItem = PatternMapper<OrderItemMapper>.Instance.MapToResponse(orderItem);
                    return new Response
                    {
                        StatusCode = 201,
                        Message = "Order item created successfully",
                        Data = responseOrderItem
                    };
                }
                else
                {
                    throw new Exception("Failed to create order item");
                }
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message.ToString(),
                };
            }
        }

        public Task<Response> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ResponseOrderItem> GetAll()
        {
            try
            {
                var listOrderItem = context.OrderItems
                    .Include(oi => oi.Dish)
                    .ToList();
                return listOrderItem
                    .Select(PatternMapper<OrderItemMapper>.Instance.MapToResponse)
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
                var existing = await context.OrderItems
                    .Include(oi => oi.Dish)
                    .Include(oi => oi.Order)
                    .FirstOrDefaultAsync(oi => oi.Id.ToString() == id);
                if (existing == null || existing.Order.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Order item not found",
                    };
                }
                var responseOrderItem = PatternMapper<OrderItemMapper>.Instance.MapToResponse(existing);
                return new Response
                {
                    StatusCode = 200,
                    Message = "Order item retrieved successfully",
                    Data = responseOrderItem
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message.ToString(),
                };
            }
        }

        public async Task<Response> Update(DTOOrderItemUpdate orderItemUpdate)
        {
            try
            {
                var existing = await context.OrderItems
                    .Include(oi => oi.Order)
                    .FirstOrDefaultAsync(oi => oi.Id.ToString() == orderItemUpdate.Id);
                if (existing == null || existing.Order.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Order item not found",
                    };
                }
                existing.Quantity = orderItemUpdate.Quantity;
                existing.UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                context.OrderItems.Update(existing);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    var responseOrderItem = PatternMapper<OrderItemMapper>.Instance.MapToResponse(existing);
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Order item updated successfully",
                        Data = responseOrderItem
                    };
                }
                else
                {
                    throw new Exception("Failed to update order item");
                }
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message.ToString(),
                };
            }
        }
    }
}
