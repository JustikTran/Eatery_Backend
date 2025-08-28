using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.Mapper;
using Eatery_API.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;

namespace Eatery_API.Infrastructures.Providers
{
    public class UserProvider : IUserProvider
    {
        private AppDbContext context;
        public UserProvider(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentException(nameof(context));
        }
        public async Task<Response> Create(DTOUserCreate userCreate)
        {
            try
            {
                var user = PatternMapper<UserMapper>.Instance.MapToEntity(userCreate);
                context.Users.Add(user);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 201,
                        Message = "Create user successfully",
                        Data = PatternMapper<UserMapper>.Instance.MapToResponse(user)
                    };
                }
                else
                {
                    throw new Exception("Create user failed");
                }
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message,
                };
            }
        }

        public async Task<Response> Delete(string id)
        {
            try
            {
                var existingUser = await context.Users.FindAsync(Guid.Parse(id));
                if (existingUser == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }
                if (existingUser.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 410,
                        Message = "Account has been deleted."
                    };
                }
                existingUser.IsDeleted = true;
                existingUser.UpdatedAt = DateTime.Now;
                context.Users.Update(existingUser);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Delete user successfully",
                        Data = PatternMapper<UserMapper>.Instance.MapToResponse(existingUser)
                    };
                }
                else
                {
                    throw new Exception("Delete user failed");
                }
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message,
                };
            }
        }

        public IQueryable<ResponseUser> GetAll()
        {
            try
            {
                var listUser = context.Users.ToList();
                return listUser
                    .Select(PatternMapper<UserMapper>.Instance.MapToResponse)
                    .AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> GetByAccountId(string accountId)
        {
            try
            {
                var user = await context.Users
                    .FirstOrDefaultAsync(u => u.AccountId.ToString() == accountId);
                if (user == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }
                if (user.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 410,
                        Message = "User is deleted"
                    };
                }
                return new Response
                {
                    StatusCode = 200,
                    Message = "Get user successfully",
                    Data = PatternMapper<UserMapper>.Instance.MapToResponse(user)
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message,
                };
            }
        }

        public async Task<Response> GetById(string id)
        {
            try
            {
                var user = await context.Users.FindAsync(Guid.Parse(id));
                if (user == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }
                if (user.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 410,
                        Message = "User is deleted"
                    };
                }
                return new Response
                {
                    StatusCode = 200,
                    Message = "Get user successfully",
                    Data = PatternMapper<UserMapper>.Instance.MapToResponse(user)
                };
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message,
                };
            }
        }

        public async Task<Response> Update(DTOUserUpdate userUpdate)
        {
            try
            {
                var existingUser = await context.Users.FindAsync(Guid.Parse(userUpdate.Id));
                if (existingUser == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "User not found"
                    };
                }
                if (existingUser.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 410,
                        Message = "Account has been deleted."
                    };
                }
                existingUser.Avatar = userUpdate.Avatar;
                existingUser.DateOfBirth = userUpdate.DateOfBirth;
                existingUser.FirstName = userUpdate.FirstName;
                existingUser.LastName = userUpdate.LastName;
                existingUser.Language = userUpdate.Language;
                existingUser.UpdatedAt = DateTime.Now;
                context.Users.Update(existingUser);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Update user successfully",
                        Data = PatternMapper<UserMapper>.Instance.MapToResponse(existingUser)
                    };
                }
                else
                {
                    throw new Exception("Update user failed");
                }
            }
            catch (Exception err)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = err.Message,
                };
            }
        }
    }
}
