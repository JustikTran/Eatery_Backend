using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Helpers.VNPay;
using Microsoft.AspNetCore.Mvc;

namespace Eatery_API.Controllers
{
    [Route("payment")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IVNPayHelper VNPayHelper;
        private readonly IConfiguration config;
        public PaymentController(IVNPayHelper vNPayHelper, IConfiguration config)
        {
            VNPayHelper = vNPayHelper;
            this.config = config;
        }

        [HttpGet("vnpay-return")]
        public ActionResult VNPayReturn()
        {
            var response = VNPayHelper.PaymentExecute(HttpContext.Request.Query);
            return response.VnPayResponseCode == "00" ? Redirect(config["Redirect:UrlSuccess"]!) : Redirect(config["Redirect:UrlFail"]!);
        }


        [HttpPost("create-vnpay")]
        public IActionResult CreateVNPay([FromBody] VNPayRequest request)
        {
            var paymentUrl = VNPayHelper.CreatePaymentUrl(request, HttpContext);
            return Ok(new { url = paymentUrl });
        }
    }
}
