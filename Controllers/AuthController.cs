using Application.Contratcs.Services;
using Domain.DTO;
using FullCartAPI.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route(APIConstants.Auth.RegisterUser)]

        public async Task<IActionResult> RegisterUserAsync(SignUpDTO request)
        {
            return await HandleRequestAsync( async () =>
            {
                var res = await _authService.SignUpAsync(request);
                return Ok(res);
            });
        }

        private async Task<IActionResult> HandleRequestAsync(Func<Task<IActionResult>> action)
        {
            try
            {
                return await action.Invoke();
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }
    }
}
