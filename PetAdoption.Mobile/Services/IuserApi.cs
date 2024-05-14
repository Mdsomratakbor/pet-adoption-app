using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Mobile.Services
{
    [Headers("Authorization: Bearer")] // Adding the header to each request for facts
    public interface IUserApi
    {
        [Post("/api/user/adopt/{petId}")]
        public Task<ApiResponse> AdoptPetAsync(int petId);
        [Get("/api/user/adoptions")]
        public Task<ApiResponseDto<PetListDto[]>> GetUserAdoptionPetsAsync();
        [Get("/api/user/favorites")]
        public Task<ApiResponseDto<PetListDto[]>> GetUserFavoritePetsAsync();

        [Post("/api/user/favorites/{petid}")]
        public Task<ApiResponse> ToggleFavoritesAsync(int petId);

        [Get("/api/user/view-pet-details/{petId}")]
        Task<ApiResponseDto<PetDetailDto>> GetPetsDetailsAsync(int petId);
    }
}
