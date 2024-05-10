using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.Api.Services.Interfaces;
using PetAdoption.Shared.Dtos;

namespace PetAdoption.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        public PetsController(IPetService petService)
        {
            _petService = petService;
        }

        [HttpGet("")]
        public async Task<ApiResponseDto<PetListDto[]>> GetAllPetsAsync() =>
            await _petService.GetAllPetsAsync();

        [HttpGet("new/{count:int}")]
        public async Task<ApiResponseDto<PetListDto[]>> GetNewlyAddedPetsAsync(int count) =>
            await _petService.GetNewlyAddedPetsAsync(count);


        [HttpGet("{petId:int}")]
        public async Task<ApiResponseDto<PetDetailDto>> GetPetsDetailsAsync(int petId) =>
            await _petService.GetPetsDetailsAsync(petId);


        [HttpGet("popular/{count:int}")]
        public async Task<ApiResponseDto<PetListDto[]>> GetPopularPetsAsync(int count) =>
            await _petService.GetPopularPetsAsync(count);

        [HttpGet("random/{count:int}")]
        public async Task<ApiResponseDto<PetListDto[]>> GetRandomPetsAsync(int count) =>
            await _petService.GetRandomPetsAsync(count);

    }
}
