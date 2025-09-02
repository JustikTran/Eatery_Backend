using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eatery_API.Controllers
{
    [Route("odata/topping")]
    [ApiController]
    public class ToppingController : ODataController
    {
        private IToppingProvider toppingProvider;

        public ToppingController(IToppingProvider toppingProvider)
        {
            this.toppingProvider = toppingProvider ?? throw new ArgumentException(nameof(toppingProvider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseTopping>> GetAll()
        {
            try
            {
                var listToppings = toppingProvider.GetAll();
                return Ok(listToppings.AsQueryable());
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
                var result = await toppingProvider.GetById(id);
                return StatusCode(result.StatusCode, result);
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

        [HttpPost]
        public async Task<IActionResult> CreateTopping([FromBody] DTOToppingCreate toppingCreate)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await toppingProvider.Create(toppingCreate);
                return StatusCode(result.StatusCode, result);
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

        [HttpPut("id={id}")]
        public async Task<IActionResult> UpdateTopping([FromRoute] string id, [FromBody] DTOToppingUpdate toppingUpdate)
        {
            try
            {
                if (id != toppingUpdate.Id) return BadRequest("ID in route does not match ID in body");
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await toppingProvider.Update(toppingUpdate);
                return StatusCode(result.StatusCode, result);
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

        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteTopping([FromRoute] string id)
        {
            try
            {
                var result = await toppingProvider.Delete(id);
                return StatusCode(result.StatusCode, result);
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

    }
}
