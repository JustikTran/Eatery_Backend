using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;

namespace Eatery_API.Domain.Interfaces
{
    public interface IDishProvider
    {
        IQueryable<ResponseDish> GetAll();
        Task<Response> GetById(string id);
        Task<Response> Create(DTODishCreate dish);
        Task<Response> Update(DTODishUpdate dish);
        Task<Response> Delete(string id);
    }
}
