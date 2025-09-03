using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Entities;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.Authentication;
using Eatery_API.Helpers.Mapper;
using Eatery_API.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;

namespace Eatery_API.Infrastructures.Providers
{
    public class AuthProvider : IAuthProvider
    {
        private AppDbContext context;
        public AuthProvider(AppDbContext _context)
        {
            context = _context ?? throw new ArgumentException(nameof(_context));
        }

        public async Task<Response> SignIn(DTOSignIn data)
        {
            try
            {
                var existing = await context.Accounts
                    .FirstOrDefaultAsync(a => a.Username == data.Username
                    || a.Email == data.Username
                    || a.Id.ToString() == data.Username);
                if (existing == null)
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Account not found"
                    };
                if (!BCrypt.Net.BCrypt.Verify(data.Password, existing.Password))
                    return new Response
                    {
                        StatusCode = 401,
                        Message = "Username or account does not match."
                    };
                if (existing.IsBanned)
                    return new Response
                    {
                        StatusCode = 403,
                        Message = "Account is banned."
                    };
                var token = TokenGenerator.Instance.GetToken(existing);
                return new Response
                {
                    StatusCode = 200,
                    Message = "Sign in successfully.",
                    Data = new
                    {
                        Token = token,
                        Account = PatternMapper<AccountMapper>.Instance.MapToResponse(existing)
                    }
                };
            }
            catch (Exception)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = "Internal server error."
                };
            }
        }

        public async Task<Response> SignUp(DTOAccountCreate data)
        {
            try
            {
                var existing = await context.Accounts
                    .FirstOrDefaultAsync(a => a.Username == data.Username
                    || a.Email == data.Email);
                if (existing != null)
                    return new Response
                    {
                        StatusCode = 409,
                        Message = "Username or email already exists."
                    };
                var account = PatternMapper<AccountMapper>.Instance.MapToEntity(data);
                context.Accounts.Add(account);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 201,
                        Message = "Account created successfully. Please wait for activation.",
                        Data = new
                        {
                            username = account.Username,
                        }
                    };
                }
                else
                {
                    return new Response
                    {
                        StatusCode = 400,
                        Message = "Account creation failed."
                    };
                }
            }
            catch (Exception)
            {
                return new Response
                {
                    StatusCode = 500,
                    Message = "Internal server error."
                };
            }
        }
    }
}
