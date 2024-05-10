

using Refit;

namespace PetAdoption.Mobile.Services
{
    public interface IAuthApi
    {
        Task<ApiResponse<AuthResponseDto>> Login(LoginRequestDto dto);
        Task<ApiResponseDto<AuthResponseDto>> Register(RegisterRequestDto dto);
    }
}
