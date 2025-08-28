using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;

namespace Eatery_API.Domain.Interfaces
{
    public interface IAccountProvider
    {
        IQueryable<ResponseAccount> GetAll();
        Task<Response> GetById(string id);
        Task<Response> GetByUsername(string username);
        Task<Response> GetByEmail(string email);
        Task<Response> GetByPhoneNumber(string phoneNumber);
        Task<Response> IsUsernameExist(string username);
        Task<Response> IsEmailExist(string email);
        Task<Response> IsPhoneNumberExist(string phoneNumber);
        Task<Response> UpdateAccount(DTOAccountUpdate update);
        Task<Response> DeleteAccount(string id);
        Task<Response> ChangePassword(DTOAccountChangePassword changePassword);
    }
}
