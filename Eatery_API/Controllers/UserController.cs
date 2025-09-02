using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eatery_API.Controllers
{
    [Route("odata/user")]
    [ApiController]
    [Authorize(Roles = "USER")]
    public class UserController : ODataController
    {
        private IUserProvider userProvider;
        public UserController(IUserProvider userProvider)
        {
            this.userProvider = userProvider ?? throw new ArgumentException(nameof(userProvider));
        }

        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "ADMIN")]
        public ActionResult<IEnumerable<ResponseUser>> GetAll()
        {
            try
            {
                var listUsers = userProvider.GetAll();
                return Ok(listUsers.AsQueryable());
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message,
                });
            }
        }

        [HttpGet("id={id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var response = await userProvider.GetById(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception err)
            {
                return StatusCode(500,
                    new
                    {
                        statusCode = 500,
                        message = err.Message,
                    });
            }
        }

        [HttpGet("account-id={accountId}")]
        public async Task<IActionResult> GetByAccountId([FromRoute] string accountId)
        {
            try
            {
                var response = await userProvider.GetByAccountId(accountId);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message,
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] DTOUserCreate create)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var response = await userProvider.Create(create);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception err)
            {

                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message,
                });
            }
        }

        [HttpPut("id={id}")]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, [FromBody] DTOUserUpdate update)
        {
            try
            {
                if (id != update.Id)
                    return BadRequest(new
                    {
                        statusCode = 400,
                        message = "Id in route is not the same as id in body"
                    });
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var response = await userProvider.Update(update);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message,
                });
            }
        }

        [HttpDelete("id={id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            try
            {
                var response = await userProvider.Delete(id);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception err)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = err.Message,
                });
            }
        }

    }
}
