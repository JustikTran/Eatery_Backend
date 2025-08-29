using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Eatery_API.Helpers.Mapper;
using Eatery_API.Infrastructures.Data;

namespace Eatery_API.Infrastructures.Providers
{
    public class PaymentMethodProvider : IPaymentMethodProvider
    {
        private AppDbContext context;
        public PaymentMethodProvider(AppDbContext _context)
        {
            context = _context;
        }
        public async Task<Response> Create(DTOPaymentMethodCreate methodCreate)
        {
            try
            {
                var method = PatternMapper<PaymentMethodMapper>.Instance.MapToEntity(methodCreate);
                context.PaymentMethods.Add(method);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    var response = PatternMapper<PaymentMethodMapper>.Instance.MapToResponse(method);
                    return new Response
                    {
                        StatusCode = 201,
                        Message = "Create payment method successfully",
                        Data = response
                    };
                }
                else
                {
                    throw new Exception("Create payment method failed");
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

        public async Task<Response> Delete(string id)
        {
            try
            {
                var existing = await context.PaymentMethods.FindAsync(Guid.Parse(id));
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Payment method not found",
                        Data = null
                    };
                }
                if (!existing.IsActive)
                {
                    return new Response
                    {
                        StatusCode = 417,
                        Message = "Payment method is already inactive",
                        Data = null
                    };
                }
                existing.IsActive = false;
                context.PaymentMethods.Update(existing);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    var response = PatternMapper<PaymentMethodMapper>.Instance.MapToResponse(existing);
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Delete payment method successfully",
                        Data = response
                    };
                }
                else
                {
                    throw new Exception("Delete payment method failed");
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

        public IQueryable<ResponsePaymentMethod> GetAll()
        {
            try
            {
                var list = context.PaymentMethods.ToList();
                return list
                    .Select(PatternMapper<PaymentMethodMapper>.Instance.MapToResponse)
                    .AsQueryable();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Response> GetById(string id)
        {
            try
            {
                var existing = await context.PaymentMethods.FindAsync(Guid.Parse(id));
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Payment method not found",
                        Data = null
                    };
                }
                var response = PatternMapper<PaymentMethodMapper>.Instance.MapToResponse(existing);
                return new Response
                {
                    StatusCode = 200,
                    Message = "Get payment method successfully",
                    Data = response
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

        public async Task<Response> Update(DTOPaymentMethodUpdate methodUpdate)
        {
            try
            {
                var existing = await context.PaymentMethods.FindAsync(Guid.Parse(methodUpdate.Id));
                if (existing == null)
                {
                    return new Response
                    {
                        StatusCode = 404,
                        Message = "Payment method not found",
                        Data = null
                    };
                }
                existing.Name = methodUpdate.Name;
                existing.MethodCode = methodUpdate.MethodCode;
                existing.UpdatedAt = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Unspecified);

                context.PaymentMethods.Update(existing);
                var result = await context.SaveChangesAsync();
                if (result > 0)
                {
                    var response = PatternMapper<PaymentMethodMapper>.Instance.MapToResponse(existing);
                    return new Response
                    {
                        StatusCode = 200,
                        Message = "Update payment method successfully",
                        Data = response
                    };
                }
                else
                {
                    throw new Exception("Update payment method failed");
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
