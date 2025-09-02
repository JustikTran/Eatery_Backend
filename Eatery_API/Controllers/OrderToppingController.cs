using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eatery_API.Controllers
{
    [Route("odata/order-topping")]
    [ApiController]
    [Authorize]
    public class OrderToppingController : ODataController
    {
        private readonly IOrderToppingProvider _orderToppingProvider;
        public OrderToppingController(IOrderToppingProvider orderToppingProvider)
        {
            _orderToppingProvider = orderToppingProvider ?? throw new ArgumentException(nameof(orderToppingProvider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseOrderTopping>> GetAll()
        {
            try
            {
                var result = _orderToppingProvider.GetAll();
                return Ok(result);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    message = err.Message,
                    statusCode = 500
                });
            }
        }
        [HttpGet("id={id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var result = await _orderToppingProvider.GetById(id);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    message = err.Message.ToString(),
                    statusCode = 500
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DTOOrderToppingCreate orderToppingCreate)
        {
            try
            {
                var result = await _orderToppingProvider.CreateOrderTopping(orderToppingCreate);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    message = err.Message.ToString(),
                    statusCode = 500
                });
            }
        }
    }
}
