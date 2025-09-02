using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;

namespace Eatery_API.Helpers.Mapper
{
    public class UserMapper
    {
        public User MapToEntity(DTOUserCreate userCreate)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = userCreate.FirstName,
                LastName = userCreate.LastName,
                Avatar = userCreate.Avatar,
                DateOfBirth = userCreate.DateOfBirth,
                Language = userCreate.Language,
                AccountId = Guid.Parse(userCreate.AccountId),
                IsDeleted = userCreate.IsDeleted,
            };
        }

        public ResponseUser MapToResponse(User? user)
        {
            return new ResponseUser
            {
                Id = user?.Id.ToString(),
                FirstName = user?.FirstName,
                LastName = user?.LastName,
                Avatar = user?.Avatar,
                DateOfBirth = user?.DateOfBirth ?? default,
                Language = user?.Language,
                AccountId = user?.AccountId.ToString(),
                IsDeleted = user?.IsDeleted ?? false,
                CreateAt = user?.CreatedAt ?? default,
                UpdateAt = user?.UpdatedAt ?? default
            };
        }
    }
}
