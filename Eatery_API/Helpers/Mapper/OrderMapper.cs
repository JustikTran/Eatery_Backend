using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;

namespace Eatery_API.Helpers.Mapper
{
    public class OrderMapper
    {
        public Order MapToEntity(DTOOrderCreate orderCreate)
        {
            return new Order
            {
                Id = Guid.NewGuid(),
                UserId = Guid.Parse(orderCreate.UserId),
                Status = orderCreate.Status,
                TotalPrice = orderCreate.TotalPrice,
                AddressId = Guid.Parse(orderCreate.AddressId),
                Paid = orderCreate.Paid,
                PaidAt = orderCreate.PaidAt,
                PaymentMethodId = Guid.Parse(orderCreate.PaymentMethodId),
                OrderItems = orderCreate.OrderItems != null ? orderCreate.OrderItems
                    .Select(PatternMapper<OrderItemMapper>.Instance.MapToEntity)
                    .ToList() : new List<OrderItem>(),
                IsDeleted = false
            };
        }

        public ResponseOrder MapToResponse(Order? order)
        {
            return new ResponseOrder {
                Id = order?.Id.ToString(),
                UserId = order?.UserId.ToString(),
                User = order?.User != null ? PatternMapper<UserMapper>.Instance.MapToResponse(order.User) : null,
                Status = order?.Status,
                TotalPrice = order?.TotalPrice ?? 0,
                AddressId = order?.AddressId.ToString(),
                Address = order?.Address != null ? PatternMapper<UserAddressMapper>.Instance.MapToResponse(order.Address) : null,
                Paid = order?.Paid ?? false,
                PaidAt = order?.PaidAt ?? DateTime.MinValue,
                PaymentMethodId = order?.PaymentMethodId.ToString(),
                //PaymentMethod = order?.PaymentMethod != null ? PatternMapper<PaymentMethodMapper>.Instance.MapToResponse(order.PaymentMethod) : null,
                OrderItems = order?.OrderItems != null ? order.OrderItems
                    .Select(PatternMapper<OrderItemMapper>.Instance.MapToResponse)
                    .ToList() : new List<ResponseOrderItem>(),
                CreateAt = order?.CreatedAt ?? DateTime.MinValue,
                UpdateAt = order?.UpdatedAt ?? DateTime.MinValue,
            };
        }
    }
}
