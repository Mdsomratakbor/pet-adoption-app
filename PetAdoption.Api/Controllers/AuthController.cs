using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.Api.Services.Interfaces;
using PetAdoption.Shared.Dtos;

namespace PetAdoption.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authSevice;
        public AuthController(IAuthService authSevice)
        {
            _authSevice = authSevice;
        }

        [HttpPost("login")]
        public async Task<ApiResponseDto<AuthResponseDto>> Login(LoginRequestDto dto)=>
            await _authSevice.LoginAsync(dto);

        [HttpPost("register")]
        public async Task<ApiResponseDto<AuthResponseDto>> Register(RegisterRequestDto dto) =>
            await _authSevice.RegisterAsync(dto);

    }
}
