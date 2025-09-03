using Eatery_API.Domain.DTOs.Request;
using Eatery_API.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Eatery_API.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IAuthProvider authProvider;
        public AuthController(IAuthProvider _authProvider)
        {
            authProvider = _authProvider ?? throw new ArgumentException(nameof(_authProvider));
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> Login([FromBody] DTOSignIn request)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        statusCode = 400,
                        message = "Invalid request data.",
                    });
                var result = await authProvider.SignIn(request);
                return StatusCode(result.StatusCode, result);
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    statusCode = 500,
                    message = "An error occurred while processing your request."
                });
            }
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] DTOAccountCreate accountCreate)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(new
                    {
                        statusCode = 400,
                        message = "Invalid request data.",
                    });
                var response = await authProvider.SignUp(accountCreate);
                return StatusCode(response.StatusCode, response);
            }
            catch (Exception)
            {
                return StatusCode(500,
                    new
                    {
                        statusCode = 500,
                        message = "An error occurred while processing your request."
                    });
            }
        }

    }
}
