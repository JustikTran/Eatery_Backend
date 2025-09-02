using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;

namespace Eatery_API.Helpers.Mapper
{
    public class AccountMapper
    {
        public Account MapToEntity(DTOAccountCreate create)
        {
            return new Account
            {
                Id = Guid.NewGuid(),
                Username = create.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(create.Password),
                Role = create.Role ?? "USER",
                Email = create.Email,
                PhoneNumber = create.PhoneNumber,
                IsActived = false,
                IsBanned = false,
                IsDeleted = false,
            };
        }

        public ResponseAccount MapToResponse(Account? account)
        {
            return new ResponseAccount
            {
                Id = account?.Id.ToString(),
                Username = account?.Username,
                Email = account?.Email,
                PhoneNumber = account?.PhoneNumber,
                Role = account?.Role,
                CreatedAt = account?.CreatedAt ?? DateTime.MinValue,
                UpdatedAt = account?.UpdatedAt ?? DateTime.MinValue,
                IsActived = account?.IsActived ?? false,
                IsBanned = account?.IsBanned ?? false,
                IsDeleted = account?.IsDeleted ?? false
            };
        }
    }
}
