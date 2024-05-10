
namespace PetAdoption.Mobile.Services
{
    public interface IAuthApi
    {
        [Post("/api/auth/login")]
        Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginRequestDto dto);

        [Post("/api/auth/register")]
        Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterRequestDto dto);
    }

   
}
