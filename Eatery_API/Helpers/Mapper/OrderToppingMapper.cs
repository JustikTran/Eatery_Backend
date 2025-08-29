using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;

namespace Eatery_API.Helpers.Mapper
{
    public class OrderToppingMapper
    {
        public OrderTopping MapToEntity(DTOOrderToppingCreate toppingCreate)
        {
            return new OrderTopping
            {
                Id = Guid.NewGuid(),
                OrderItemId = toppingCreate.OrderItemId != null ? Guid.Parse(toppingCreate.OrderItemId) : Guid.Empty,
                ToppingId = Guid.Parse(toppingCreate.ToppingId),
                Quantity = toppingCreate.Quantity,
                UnitPrice = toppingCreate.UnitPrice,
            };
        }

        public ResponseOrderTopping MapToResponse(OrderTopping? orderTopping)
        {
            return new ResponseOrderTopping
            {
                Id = orderTopping?.Id.ToString(),
                OrderItemId = orderTopping?.OrderItemId.ToString(),
                ToppingId = orderTopping?.ToppingId.ToString(),
                Topping = orderTopping?.Topping != null ? new ToppingMapper().MapToResponse(orderTopping.Topping) : null,
                Quantity = orderTopping?.Quantity ?? 0,
                UnitPrice = orderTopping?.UnitPrice ?? 0,
                CreateAt = orderTopping?.CreatedAt ?? DateTime.MinValue,
                UpdateAt = orderTopping?.UpdatedAt ?? DateTime.MinValue,
            };
        }
    }
}
