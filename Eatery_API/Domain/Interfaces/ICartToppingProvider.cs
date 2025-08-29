using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;

namespace Eatery_API.Domain.Interfaces
{
    public interface ICartToppingProvider
    {
        IQueryable<ResponseCartTopping> GetCartToppings();
        Task<Response> GetById(string id);
        Task<Response> CreateCartTopping(DTOCartToppingCreate cartToppingCreate);
        Task<Response> UpdateCartTopping(DTOCartToppingUpdate cartToppingUpdate);
        Task<Response> DeleteCartTopping(string id);
    }
}
