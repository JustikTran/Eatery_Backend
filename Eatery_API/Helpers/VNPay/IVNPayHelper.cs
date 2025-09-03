using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;

namespace Eatery_API.Helpers.VNPay
{
    public interface IVNPayHelper
    {
        string CreatePaymentUrl(VNPayRequest model, HttpContext context);
        ResponseVNPay PaymentExecute(IQueryCollection collections);

    }
}
