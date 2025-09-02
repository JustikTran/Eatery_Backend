using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eatery_API.Controllers
{
    [Route("odata/order")]
    [ApiController]
    [Authorize]
    public class OrderController : ODataController
    {
        private readonly IOrderProvider orderProvider;
        public OrderController(IOrderProvider _orderProvider)
        {
            orderProvider = _orderProvider ?? throw new ArgumentException(nameof(_orderProvider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseOrder>> GetAll()
        {
            try
            {
                var listOrder = orderProvider.GetAll();
                return Ok(listOrder.AsQueryable());
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
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var response = await orderProvider.GetById(id);
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
        public async Task<IActionResult> CreateOrder([FromBody] DTOOrderCreate orderCreate)
        {
            try
            {
                var response = await orderProvider.Create(orderCreate);
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

        [HttpPut("id={id}")]
        public async Task<IActionResult> UpdateOrder([FromRoute]string id, [FromBody] DTOOrderUpdate orderUpdate)
        {
            try
            {
                var response = await orderProvider.Update(orderUpdate);
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

        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteOrder([FromRoute]string id)
        {
            try
            {
                var response = await orderProvider.Delete(id);
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
