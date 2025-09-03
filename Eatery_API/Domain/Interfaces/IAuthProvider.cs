using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;

namespace Eatery_API.Domain.Interfaces
{
    public interface IAuthProvider
    {
        Task<Response> SignIn(DTOSignIn data);
        Task<Response> SignUp(DTOAccountCreate data);
    }
}
