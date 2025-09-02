using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eatery_API.Controllers
{
    [Route("odata/cart")]
    [ApiController]
    [Authorize]
    public class CartController : ODataController
    {
        private readonly ICartProvider cartProvider;
        public CartController(ICartProvider _cartProvider)
        {
            cartProvider = _cartProvider ?? throw new ArgumentException(nameof(_cartProvider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseCart>> GetAll()
        {
            try
            {
                var listCarts = cartProvider.GetCarts();
                return Ok(listCarts.AsQueryable());
            }
            catch (Exception err)
            {

                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message
                });
            }
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var response = await cartProvider.GetById(id);
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
        public async Task<IActionResult> CreateCart([FromBody] DTOCartCreate cartCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { statusCode = 500, message = ModelState });
                var response = await cartProvider.CreateCart(cartCreate);
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
        public async Task<IActionResult> UpdateCart([FromRoute] string id, [FromBody] DTOCartUpdate cartUpdate)
        {
            try
            {
                if (id != cartUpdate.Id)
                    return BadRequest(new { statusCode = 400, message = "Id in body must be same as id in route" });
                if (!ModelState.IsValid)
                    return BadRequest(new { statusCode = 400, message = ModelState });
                var response = await cartProvider.UpdateCart(cartUpdate);
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
        public async Task<IActionResult> DeleteCart([FromRoute] string id)
        {
            try
            {
                var response = await cartProvider.DeleteCart(id);
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
