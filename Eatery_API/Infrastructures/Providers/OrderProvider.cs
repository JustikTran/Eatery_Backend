using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.Mapper;
using Eatery_API.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;

namespace Eatery_API.Infrastructures.Providers
{
    public class OrderProvider : IOrderProvider
    {
        private AppDbContext context;
        public OrderProvider(AppDbContext _context)
        {
            context = _context ?? throw new ArgumentException(nameof(_context));
        }
        public async Task<Response> Create(DTOOrderCreate orderCreate)
        {
            try
            {
                var order = PatternMapper<OrderMapper>.Instance.MapToEntity(orderCreate);
                context.Orders.Add(order);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    var responseOrder = PatternMapper<OrderMapper>.Instance.MapToResponse(order);
                    return new Response
                    {
                        StatusCode = 201,
                        Message = "Order created successfully",
                        Data = responseOrder
                    };
                }
                else
                {
                    throw new Exception("Failed to create order");
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

        public async Task<Response> Delete(string id)
        {
            try
            {
                var existing = await context.Orders.FindAsync(Guid.Parse(id));
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Order not found",
                    };
                }
                if (existing.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 410,
                        Message = "Order already deleted",
                    };
                }
                existing.IsDeleted = true;
                existing.UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);
                context.Orders.Update(existing);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Order deleted successfully",
                    };
                }
                else
                {
                    throw new Exception("Failed to delete order");
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

        public IQueryable<ResponseOrder> GetAll()
        {
            try
            {
                var listOrder = context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Address)
                    .Include(o => o.OrderItems)
                    .ToList();
                return listOrder
                    .Select(PatternMapper<OrderMapper>.Instance.MapToResponse)
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
                var existing = await context.Orders
                    .Include(o => o.User)
                    .Include(o => o.Address)
                    .Include(o => o.OrderItems)
                    .FirstOrDefaultAsync(o => o.Id == Guid.Parse(id));
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Order not found",
                    };
                }
                var responseOrder = PatternMapper<OrderMapper>.Instance.MapToResponse(existing);
                return new Response
                {
                    StatusCode = 200,
                    Message = "Order retrieved successfully",
                    Data = responseOrder
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

        public async Task<Response> Update(DTOOrderUpdate orderUpdate)
        {
            try
            {
                var existing = await context.Orders.FindAsync(Guid.Parse(orderUpdate.Id));
                if (existing == null || existing.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Order not found",
                    };
                }

                existing.Status = orderUpdate.Status;
                existing.Paid = orderUpdate.Paid;
                existing.PaidAt = orderUpdate.PaidAt;
                existing.UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

                context.Orders.Update(existing);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    var responseOrder = PatternMapper<OrderMapper>.Instance.MapToResponse(existing);
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Order updated successfully",
                        Data = responseOrder
                    };
                }
                else
                {
                    throw new Exception("Failed to update order");
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
