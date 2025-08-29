using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;

namespace Eatery_API.Domain.Interfaces
{
    public interface ICartProvider
    {
        IQueryable<ResponseCart> GetCarts();
        Task<Response> GetById(string id);
        Task<Response> GetByUserId(string userId);
        Task<Response> CreateCart(DTOCartCreate cartCreate);
        Task<Response> UpdateCart(DTOCartUpdate cartUpdate);
        Task<Response> DeleteCart(string id);
    }
}
