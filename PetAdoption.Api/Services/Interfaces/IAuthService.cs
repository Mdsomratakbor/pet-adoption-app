using PetAdoption.Shared.Dtos;

namespace PetAdoption.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginRequestDto dto);
        Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterRequestDto dto);

        Task<ApiResponse> ChangePasswordAsync(int userid,string newPassword);
    }
}