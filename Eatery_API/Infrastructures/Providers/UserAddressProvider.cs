using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.Mapper;
using Eatery_API.Infrastructures.Data;

namespace Eatery_API.Infrastructures.Providers
{
    public class UserAddressProvider : IUserAddressProvider
    {
        private AppDbContext context;
        public UserAddressProvider(AppDbContext _context)
        {
            context = _context ?? throw new ArgumentException(nameof(context));
        }
        public async Task<Response> AddUserAddress(DTOUserAddressCreate userAddress)
        {
            try
            {
                var userAddressEntity = PatternMapper<UserAddressMapper>.Instance.MapToEntity(userAddress);
                context.UserAddresses.Add(userAddressEntity);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 201,
                        Message = "User address created successfully",
                        Data = userAddressEntity
                    };
                }
                else
                {
                    throw new Exception("Failed to create user address");
                }
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }

        public async Task<Response> DeleteUserAddress(string userAddressId)
        {
            try
            {
                var existingAddress = await context.UserAddresses.FindAsync(Guid.Parse(userAddressId));
                if (existingAddress == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User address not found"
                    };
                }
                if (existingAddress.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 410,
                        Message = "User address is already deleted"
                    };
                }
                existingAddress.IsDeleted = true;
                context.UserAddresses.Update(existingAddress);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "User address deleted successfully"
                    };
                }
                else
                {
                    throw new Exception("Failed to delete user address");
                }
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                };
            }
        }

        public Task<Response> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<UserAddress> GetUserAddresses()
        {
            try
            {
                var listAddresses = context.UserAddresses.ToList();
                return listAddresses
                    .AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Task<Response> UpdateUserAddress(DTOUserAddressUpdate userAddress)
        {
            throw new NotImplementedException();
        }
    }
}
