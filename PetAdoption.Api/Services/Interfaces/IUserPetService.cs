using PetAdoption.Shared.Dtos;

namespace PetAdoption.Api.Services.Interfaces
{
    public interface IUserPetService
    {
        Task<ApiResponse> AdoptPetAsync(int userId, int petId);
        Task<ApiResponseDto<PetListDto[]>> GetUserAdoptionPetsAsync(int userId);
        Task<ApiResponseDto<PetListDto[]>> GetUserFavoritePetsAsync(int userId);
        Task<ApiResponse> ToggleFavoritesAsync(int userId, int petId);
    }
}