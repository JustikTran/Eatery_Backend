using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;

namespace Eatery_API.Domain.Interfaces
{
    public interface IOrderProvider
    {
        IQueryable<ResponseOrder> GetAll();
        Task<Response> GetById(string id);
        Task<Response> Create(DTOOrderCreate orderCreate);
        Task<Response> Update(DTOOrderUpdate orderUpdate);
        Task<Response> Delete(string id);
    }
}
