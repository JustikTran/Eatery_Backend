using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;

namespace Eatery_API.Domain.Interfaces
{
    public interface IUserProvider
    {
        IQueryable<ResponseUser> GetAll();
        Task<Response> GetById(string id);
        Task<Response> GetByAccountId(string accountId);
        Task<Response> Delete(string id);
        Task<Response> Update(DTOUserUpdate userUpdate);
        Task<Response> Create(DTOUserCreate userCreate);
    }
}
