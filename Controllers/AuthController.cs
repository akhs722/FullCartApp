using Application.Contratcs.Services;
using Domain.DTO;
using FullCartAPI.BaseClasses;
using FullCartAPI.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullCartAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ExceptionHandler
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

        [HttpPost]
        [Route(APIConstants.Auth.SignIn)]

        public async Task<IActionResult> SignInAsync(SignInDTO request)
        {
            return await HandleRequestAsync(async () =>
            {
                var res = await _authService.SignInAsync(request);
                return Ok(res);
            });
        }

    }
}
