using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;

namespace Eatery_API.Domain.Interfaces
{
    public interface IPaymentMethodProvider
    {
        IQueryable<ResponsePaymentMethod> GetAll();
        Task<Response> GetById(string id);
        Task<Response> Create(DTOPaymentMethodCreate methodCreate);
        Task<Response> Update(DTOPaymentMethodUpdate methodUpdate);
        Task<Response> Delete(string id);
    }
}
