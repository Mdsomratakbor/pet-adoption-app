using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Mobile.Services
{
    public interface IPetsApi
    {
        [Get("/api/pets/")]
        public Task<ApiResponseDto<PetListDto[]>> GetAllPetsAsync();


        [Get("/api/pets/new/{count}")]
        public Task<ApiResponseDto<PetListDto[]>> GetNewlyAddedPetsAsync(int count);



        [Get("/api/pets/{petId}")]
        public Task<ApiResponseDto<PetDetailDto>> GetPetsDetailsAsync(int petId);



        [Get("/api/pets/popular/{count}")]
        public Task<ApiResponseDto<PetListDto[]>> GetPopularPetsAsync(int count);

        [Get("/api/pets/random/{count}")]
        public Task<ApiResponseDto<PetListDto[]>> GetRandomPetsAsync(int count);

    }
}
