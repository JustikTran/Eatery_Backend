using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;

namespace Eatery_API.Helpers.Mapper
{
    public class OrderItemMapper
    {
        public OrderItem MapToEntity(DTOOrderItemCreate itemCreate)
        {
            return new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = itemCreate.OrderId != null ? Guid.Parse(itemCreate.OrderId) : Guid.Empty,
                DishId = Guid.Parse(itemCreate.DishId),
                Quantity = itemCreate.Quantity,
                UnitPrice = itemCreate.UnitPrice,
            };
        }
        public ResponseOrderItem MapToResponse(OrderItem? orderItem)
        {
            return new ResponseOrderItem
            {
                Id = orderItem?.Id.ToString(),
                OrderId = orderItem?.OrderId.ToString(),
                DishId = orderItem?.DishId.ToString(),
                Dish = orderItem?.Dish != null ? PatternMapper<DishMapper>.Instance.MapToResponse(orderItem.Dish) : null,
                Quantity = orderItem?.Quantity ?? 0,
                UnitPrice = orderItem?.UnitPrice ?? 0,
                OrderToppings = orderItem?.OrderToppings != null ? orderItem.OrderToppings
                .Select(PatternMapper<OrderToppingMapper>.Instance.MapToResponse)
                .ToList() : new List<ResponseOrderTopping>(),
                CreateAt = orderItem?.CreatedAt ?? DateTime.MinValue,
                UpdateAt = orderItem?.UpdatedAt ?? DateTime.MinValue,
            };
        }
    }
}
