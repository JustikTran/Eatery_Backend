using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eatery_API.Controllers
{
    [Route("cart-topping")]
    [ApiController]
    public class CartToppingController : ODataController
    {
        private readonly ICartToppingProvider cartToppingProvider;
        public CartToppingController(ICartToppingProvider _cartToppingProvider)
        {
            cartToppingProvider = _cartToppingProvider ?? throw new ArgumentException(nameof(_cartToppingProvider));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ResponseCartTopping>> GetAll()
        {
            try
            {
                var listCartToppings = cartToppingProvider.GetCartToppings();
                return Ok(listCartToppings.AsQueryable());
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
                var response = await cartToppingProvider.GetById(id);
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
        public async Task<IActionResult> CreateCartTopping([FromBody] DTOCartToppingCreate cartToppingCreate)
        {
            try
            {
                var response = await cartToppingProvider.CreateCartTopping(cartToppingCreate);
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
        public async Task<IActionResult> UpdateCartTopping([FromRoute] string id, [FromBody] DTOCartToppingUpdate cartToppingUpdate)
        {
            try
            {
                if (id != cartToppingUpdate.Id)
                    return BadRequest(new { statusCode = 400, message = "Id in URL and body do not match" });
                if (!ModelState.IsValid)
                    return BadRequest(new { statusCode = 400, message = ModelState });
                var response = await cartToppingProvider.UpdateCartTopping(cartToppingUpdate);
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
        public async Task<IActionResult> DeleteCartTopping([FromRoute] string id)
        {
            try
            {
                var response = await cartToppingProvider.DeleteCartTopping(id);
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
