using PetAdoption.Shared.Dtos;

namespace PetAdoption.Api.Services.Interfaces
{
    public interface IUserPetService
    {
        Task<ApiResponseDto> AdoptPetAsync(int userId, int petId);
        Task<ApiResponseDto<PetListDto[]>> GetUserAdoptionPetsAsync(int userId);
        Task<ApiResponseDto<PetListDto[]>> GetUserFavoritePetsAsync(int userId);
        Task<ApiResponseDto> ToggleFavoritesAsync(int userId, int petId);
    }
}