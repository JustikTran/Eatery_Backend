using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eatery_API.Controllers
{
    [Route("odata/order-item")]
    [ApiController]
    public class OrderItemController : ODataController
    {
        private readonly IOrderItemProvider orderItemProvider;
        public OrderItemController(IOrderItemProvider _orderItemProvider)
        {
            orderItemProvider = _orderItemProvider ?? throw new ArgumentException(nameof(_orderItemProvider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseOrderItem>> GetAll()
        {
            try
            {
                var listOrderItem = orderItemProvider.GetAll();
                return Ok(listOrderItem.AsQueryable());
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message.ToString(),
                });
            }
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> GetOrderItem([FromRoute] string id)
        {
            try
            {
                var response = await orderItemProvider.GetById(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message.ToString(),
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderItem([FromBody] DTOOrderItemCreate orderItemCreate)
        {
            try
            {
                var response = await orderItemProvider.Create(orderItemCreate);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message.ToString(),
                });
            }
        }
    }
}
