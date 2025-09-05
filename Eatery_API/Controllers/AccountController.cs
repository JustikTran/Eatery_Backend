using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Security.Claims;

namespace Eatery_API.Controllers
{
    [Route("odata/account")]
    [ApiController]
    public class AccountController : ODataController
    {
        private IAccountProvider accountProvider;
        public AccountController(IAccountProvider accountProvider)
        {
            this.accountProvider = accountProvider ?? throw new ArgumentException(nameof(accountProvider));
        }

        [HttpGet]
        [EnableQuery]
        public ActionResult<IEnumerable<ResponseAccount>> GetAll()
        {
            try
            {
                var listAccounts = accountProvider.GetAll();
                return Ok(listAccounts.AsQueryable());
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

        [HttpGet("token")]
        [Authorize]
        public async Task<IActionResult> GetByToken()
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (userId == null)
                {
                    return Unauthorized(new
                    {
                        statusCode = 401,
                        message = "Invalid token",
                    });
                }
                var response = await accountProvider.GetById(userId);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = "Internal server error",
                });
            }
        }


        [HttpGet("id={id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            try
            {
                var response = await accountProvider.GetById(id);
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

        [HttpGet("username={username}")]
        public async Task<IActionResult> GetByUsername([FromRoute] string username)
        {
            try
            {
                var response = await accountProvider.GetByUsername(username);
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

        [HttpGet("email={email}")]
        public async Task<IActionResult> GetItem([FromRoute] string email)
        {
            try
            {
                var response = await accountProvider.GetByEmail(email);
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

        [HttpGet("phone={id}")]
        public async Task<IActionResult> GetByPhone([FromRoute] string phone)
        {
            try
            {
                var response = await accountProvider.GetByPhoneNumber(phone);
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

        [HttpPut("update/id={id}")]
        public async Task<IActionResult> UpdateAccount([FromRoute] string id, [FromBody] DTOAccountUpdate update)
        {
            try
            {
                if (id != update.Id)
                {
                    return BadRequest(new
                    {
                        statusCode = 400,
                        message = "Id in route does not match Id in body",
                    });
                }
                var response = await accountProvider.UpdateAccount(update);
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

        [HttpPut("change-passowrd/id={id}")]
        public async Task<IActionResult> ChangePassword([FromRoute] string id, [FromBody] DTOAccountChangePassword changePassword)
        {
            try
            {
                if (id != changePassword.Id)
                {
                    return BadRequest(new
                    {
                        statusCode = 400,
                        message = "Id in route does not match Id in body",
                    });
                }
                var response = await accountProvider.ChangePassword(changePassword);
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
        public async Task<IActionResult> DeleteAccount([FromRoute] string id)
        {
            try
            {
                var response = await accountProvider.DeleteAccount(id);
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
