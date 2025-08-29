using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;

namespace Eatery_API.Domain.Interfaces
{
    public interface IOrderItemProvider
    {
        IQueryable<ResponseOrderItem> GetAll();
        Task<Response> GetById(string id);
        Task<Response> Create(DTOOrderItemCreate orderItemCreate);
        Task<Response> Update(DTOOrderItemUpdate orderItemUpdate);
        Task<Response> Delete(string id);
    }
}
