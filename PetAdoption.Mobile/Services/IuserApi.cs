using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Mobile.Services
{
    [Headers("Authorization: Bearer")]
    public interface IuserApi
    {
        [Post("/api/user/adopt/{petId:int}")]
        public Task<ApiResponse> AdoptPetAsync(int petId);
        [Get("/api/user/adoptions")]
        public Task<ApiResponseDto<PetListDto[]>> GetUserAdoptionPetsAsync();
        [Get("/api/user/favorites")]
        public Task<ApiResponseDto<PetListDto[]>> GetUserFavoritePetsAsync();

        [Post("/api/user/favorites/{petid:int}")]
        public Task<ApiResponse> ToggleFavoritesAsync(int petId);
    }
}
