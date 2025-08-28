using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;

namespace Eatery_API.Domain.Interfaces
{
    public interface IUserAddressProvider
    {
        IQueryable<UserAddress> GetUserAddresses();
        Task<Response> GetById(string id);
        Task<Response> AddUserAddress(DTOUserAddressCreate userAddress);
        Task<Response> UpdateUserAddress(DTOUserAddressUpdate userAddress);
        Task<Response> DeleteUserAddress(string userAddressId);
    }
}
