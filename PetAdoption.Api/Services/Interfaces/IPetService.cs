using PetAdoption.Shared.Dtos;

namespace PetAdoption.Api.Services.Interfaces
{
    public interface IPetService
    {
        Task<ApiResponseDto<PetListDto[]>> GetAllPetsAsync();
        Task<ApiResponseDto<PetListDto[]>> GetNewlyAddedPetsAsync(int count);
        Task<ApiResponseDto<PetDetailDto>> GetPetsDetailsAsync(int petId, int userId=0);
        Task<ApiResponseDto<PetListDto[]>> GetPopularPetsAsync(int count);
        Task<ApiResponseDto<PetListDto[]>> GetRandomPetsAsync(int count);
    }
}