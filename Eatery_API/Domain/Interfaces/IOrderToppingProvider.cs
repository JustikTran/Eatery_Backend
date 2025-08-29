using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;

namespace Eatery_API.Domain.Interfaces
{
    public interface IOrderToppingProvider
    {
        IQueryable<ResponseOrderTopping> GetAll();
        Task<Response> GetById(string id);
        Task<Response> CreateOrderTopping(DTOOrderToppingCreate orderToppingCreate);
    }
}
