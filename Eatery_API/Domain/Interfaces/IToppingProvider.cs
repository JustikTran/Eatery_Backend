using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;

namespace Eatery_API.Domain.Interfaces
{
    public interface IToppingProvider
    {
        IQueryable<ResponseTopping> GetAll();
        Task<Response> GetById(string id);
        Task<Response> Create(DTOToppingCreate toppingCreate);
        Task<Response> Update(DTOToppingUpdate toppingUpdate);
        Task<Response> Delete(string id);
    }
}
