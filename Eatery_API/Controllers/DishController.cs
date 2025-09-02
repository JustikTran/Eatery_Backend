using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eatery_API.Controllers
{
    [Route("odata/dish")]
    [ApiController]
    public class DishController : ODataController
    {
        private readonly IDishProvider dishProvider;
        public DishController(IDishProvider dishProvider)
        {
            this.dishProvider = dishProvider ?? throw new ArgumentException(nameof(dishProvider));
        }

        [HttpGet]
        [EnableQuery]
        [AllowAnonymous]
        public ActionResult<IEnumerable<ResponseDish>> GetAll()
        {
            try
            {
                var listDishes = dishProvider.GetAll();
                return Ok(listDishes.AsQueryable());
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
        [AllowAnonymous]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var result = await dishProvider.GetById(id);
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
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> CreateDish([FromBody] DTODishCreate dishCreate)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await dishProvider.Create(dishCreate);
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
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> UpdateDish([FromRoute] string id, [FromBody] DTODishUpdate dishUpdate)
        {
            try
            {
                if (id != dishUpdate.Id)
                    return BadRequest(new
                    {
                        statusCode = 400,
                        message = "Id in route does not match Id in body"
                    });
                if (!ModelState.IsValid) return BadRequest(ModelState);
                var result = await dishProvider.Update(dishUpdate);
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
        [Authorize(Roles = "ADMIN")]
        public async Task<IActionResult> DeleteDish([FromRoute] string id)
        {
            try
            {
                var result = await dishProvider.Delete(id);
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
