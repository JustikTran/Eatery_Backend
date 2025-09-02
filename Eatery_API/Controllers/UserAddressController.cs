using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.DTOs.Response;
using Eatery_API.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace Eatery_API.Controllers
{
    [Route("odata/user-address")]
    [ApiController]
    [Authorize]
    public class UserAddressController : ODataController
    {
        private readonly IUserAddressProvider userAddressProvider;
        public UserAddressController(IUserAddressProvider _userAddressProvider)
        {
            userAddressProvider = _userAddressProvider ?? throw new ArgumentException(nameof(_userAddressProvider));
        }

        [HttpGet]
        public ActionResult<IEnumerable<ResponseUserAddress>> GetAll()
        {
            try
            {
                var listAddresses = userAddressProvider.GetUserAddresses();
                return Ok(listAddresses.AsQueryable());
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
                var response = await userAddressProvider.GetById(id);
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
        public async Task<IActionResult> CreateUserAddress([FromBody] DTOUserAddressCreate userAddress)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await userAddressProvider.AddUserAddress(userAddress);
                return StatusCode(result.StatusCode, result);
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
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> UpdateAddress([FromRoute] string id, [FromBody] DTOUserAddressUpdate addressUpdate)
        {
            try
            {
                if (id != addressUpdate.Id)
                    return BadRequest("ID in route does not match ID in body");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var result = await userAddressProvider.UpdateUserAddress(addressUpdate);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("id={id}")]
        [Authorize(Roles = "USER")]
        public async Task<IActionResult> DeleteUserAddress([FromRoute] string id)
        {
            try
            {
                var result = await userAddressProvider.DeleteUserAddress(id);
                return StatusCode(result.StatusCode, result);
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
