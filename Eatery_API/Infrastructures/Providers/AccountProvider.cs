using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.Mapper;
using Eatery_API.Infrastructures.Data;
using Microsoft.EntityFrameworkCore;

namespace Eatery_API.Infrastructures.Providers
{
    public class AccountProvider : IAccountProvider
    {
        private readonly AppDbContext context;
        public AccountProvider(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentException(nameof(context));
        }

        public async Task<Response> ChangePassword(DTOAccountChangePassword changePassword)
        {
            try
            {
                var existingAccount = await context.Accounts.FindAsync(changePassword.Id);
                if (existingAccount == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Account not found."
                    };
                }
                existingAccount.Password = BCrypt.Net.BCrypt.HashPassword(changePassword.Password);
                existingAccount.UpdatedAt = DateTime.Now;
                context.Accounts.Update(existingAccount);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Password changed successfully."
                    };
                }
                else
                {
                    throw new Exception("Failed to change password.");
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

        public async Task<Response> DeleteAccount(string id)
        {
            try
            {
                var existingAccount = await context.Accounts.FindAsync(id);
                if (existingAccount == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Account not found."
                    };
                }
                existingAccount.IsDeleted = true;
                existingAccount.UpdatedAt = DateTime.Now;
                context.Accounts.Update(existingAccount);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Account deleted successfully."
                    };
                }
                else
                {
                    throw new Exception("Failed to delete account.");
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

        public IQueryable<ResponseAccount> GetAll()
        {
            try
            {
                var listAccount = context.Accounts.ToList();
                return listAccount
                    .Select(PatternMapper<AccountMapper>.Instance.MapToResponse)
                    .AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> GetByEmail(string email)
        {
            try
            {
                var existingAccount = await context.Accounts.FirstOrDefaultAsync(account => account.Email == email);
                if (existingAccount == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Account not found."
                    };
                }
                if (existingAccount.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 410,
                        Message = "Account has been deleted."
                    };
                }
                var responseAccount = PatternMapper<AccountMapper>.Instance.MapToResponse(existingAccount);
                return new Response
                {
                    StatusCode = 200,
                    Message = "Account found.",
                    Data = responseAccount
                };
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

        public async Task<Response> GetById(string id)
        {
            try
            {
                var existingAccount = await context.Accounts.FindAsync(id);
                if (existingAccount == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Account not found."
                    };
                }
                if (existingAccount.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 410,
                        Message = "Account has been deleted."
                    };
                }
                var responseAccount = PatternMapper<AccountMapper>.Instance.MapToResponse(existingAccount);
                return new Response
                {
                    StatusCode = 200,
                    Message = "Account found.",
                    Data = responseAccount
                };
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

        public async Task<Response> GetByPhoneNumber(string phoneNumber)
        {
            try
            {
                var existingAccount = await context.Accounts.FirstOrDefaultAsync(account => account.PhoneNumber == phoneNumber);
                if (existingAccount == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Account not found."
                    };
                }
                if (existingAccount.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 410,
                        Message = "Account has been deleted."
                    };
                }
                var responseAccount = PatternMapper<AccountMapper>.Instance.MapToResponse(existingAccount);
                return new Response
                {
                    StatusCode = 200,
                    Message = "Account found.",
                    Data = responseAccount
                };
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

        public async Task<Response> GetByUsername(string username)
        {
            try
            {
                var existingAccount = await context.Accounts.FirstOrDefaultAsync(account => account.Username == username);
                if (existingAccount == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Account not found."
                    };
                }
                if (existingAccount.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 410,
                        Message = "Account has been deleted."
                    };
                }
                var responseAccount = PatternMapper<AccountMapper>.Instance.MapToResponse(existingAccount);
                return new Response
                {
                    StatusCode = 200,
                    Message = "Account found.",
                    Data = responseAccount
                };
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

        public async Task<Response> IsEmailExist(string email)
        {
            try
            {
                var existingAccount = await context.Accounts.FirstOrDefaultAsync(account => account.Email == email);
                if (existingAccount == null)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Email does not exist.",
                        Data = false
                    };
                }
                return new Response
                {
                    StatusCode = 200,
                    Message = "Email exists.",
                    Data = true
                };
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

        public async Task<Response> IsPhoneNumberExist(string phoneNumber)
        {
            try
            {
                var existingAccount = await context.Accounts.FirstOrDefaultAsync(account => account.PhoneNumber == phoneNumber);
                if (existingAccount == null)
                {
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Phone number does not exist.",
                        Data = false
                    };
                }
                return new Response
                {
                    StatusCode = 200,
                    Message = "Phone number exists.",
                    Data = true
                };
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

        public Task<Response> IsUsernameExist(string username)
        {
            try
            {
                var existingAccount = context.Accounts.FirstOrDefault(account => account.Username == username);
                if (existingAccount == null)
                {
                    return Task.FromResult(new Response
                    {
                        StatusCode = 200,
                        Message = "Username does not exist.",
                        Data = false
                    });
                }
                return Task.FromResult(new Response
                {
                    StatusCode = 200,
                    Message = "Username exists.",
                    Data = true
                });
            }
            catch (Exception err)
            {
                return Task.FromResult(new Response
                {
                    StatusCode = 500,
                    Message = err.Message
                });
            }
        }

        public async Task<Response> UpdateAccount(DTOAccountUpdate update)
        {
            try
            {
                var existingAccount = await context.Accounts.FindAsync(update.Id);
                if (existingAccount == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Account not found."
                    };
                }
                if (existingAccount.IsDeleted)
                {
                    return new Response
                    {
                        StatusCode = 410,
                        Message = "Account has been deleted."
                    };
                }
                existingAccount.Email = update.Email ?? existingAccount.Email;
                existingAccount.PhoneNumber = update.PhoneNumber ?? existingAccount.PhoneNumber;
                existingAccount.IsActived = update.IsAvtived;
                existingAccount.IsBanned = update.IsBanned;
                existingAccount.IsDeleted = update.IsDeleted;
                existingAccount.UpdatedAt = DateTime.Now;
                context.Accounts.Update(existingAccount);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    var responseAccount = PatternMapper<AccountMapper>.Instance.MapToResponse(existingAccount);
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Account updated successfully.",
                        Data = responseAccount
                    };
                }
                else
                {
                    throw new Exception("Failed to update account.");
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
    }
}
