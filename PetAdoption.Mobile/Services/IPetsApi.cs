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


        [Get("/api/pets/new/{count:int}")]
        public Task<ApiResponseDto<PetListDto[]>> GetNewlyAddedPetsAsync(int count);



        [Get("/api/pets/{petId:int}")]
        public Task<ApiResponseDto<PetDetailDto>> GetPetsDetailsAsync(int petId);



        [Get("/api/pets/popular/{count:int}")]
        public Task<ApiResponseDto<PetListDto[]>> GetPopularPetsAsync(int count);

        [Get("/api/pets/random/{count:int}")]
        public Task<ApiResponseDto<PetListDto[]>> GetRandomPetsAsync(int count);

    }
}
