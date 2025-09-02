using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eatery_API.Controllers
{
    [Route("odata/payment-method")]
    [ApiController]

    public class PaymentMethodController : ODataController
    {
        private readonly IPaymentMethodProvider _paymentMethodProvider;
        public PaymentMethodController(IPaymentMethodProvider paymentMethodProvider)
        {
            _paymentMethodProvider = paymentMethodProvider ?? throw new ArgumentException(nameof(paymentMethodProvider));
        }

        [HttpGet]
        [EnableQuery]
        [Authorize]
        public ActionResult<IEnumerable<ResponsePaymentMethod>> GetAll()
        {
            try
            {
                var listMerthods = _paymentMethodProvider.GetAll();
                return Ok(listMerthods.AsQueryable());
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message.ToString()
                });
            }
        }

        [HttpGet("id={id}")]
        [Authorize]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var response = await _paymentMethodProvider.GetById(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message.ToString()
                });
            }
        }

        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateMethod([FromBody] DTOPaymentMethodCreate methodCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var response = await _paymentMethodProvider.Create(methodCreate);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message.ToString()
                });
            }
        }

        [HttpPut("id={id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateMethod([FromRoute] string id, [FromBody] DTOPaymentMethodUpdate methodUpdate)
        {
            try
            {
                if (id != methodUpdate.Id)
                {
                    return BadRequest(new
                    {
                        statusCode = 400,
                        message = "Id in route and body do not match"
                    });
                }
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var response = await _paymentMethodProvider.Update(methodUpdate);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message.ToString()
                });
            }
        }

        [HttpDelete("id={id}")]
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteMethod([FromRoute] string id)
        {
            try
            {
                var response = await _paymentMethodProvider.Delete(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message.ToString()
                });
            }
        }

    }
}
