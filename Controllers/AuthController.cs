using Application.Contratcs.Services;
using Domain.DTO;
using FullCartAPI.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullCartAPI.Controllers
{
    //[Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
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

             var res = await _authService.SignUpAsync(request);
             return Ok(res);
            
        }

        [HttpPost]
        [Route(APIConstants.Auth.SignIn)]

        public async Task<IActionResult> SignInAsync(SignInDTO request)
        {
              var res = await _authService.SignInAsync(request);
              return Ok(res);
           
        }

    }
}
