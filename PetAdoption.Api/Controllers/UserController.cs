using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.Api.Services.Interfaces;
using PetAdoption.Shared.Dtos;
using System.Security.Claims;

namespace PetAdoption.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserPetService _userPetService;
        public UserController(IUserPetService userPetService)
        {
            _userPetService = userPetService;
        }

        /// HAKE: THERE HAVE A ISSUE
        private int userId => Convert.ToInt32(User.Claims.First(c=>c.Type == ClaimTypes.NameIdentifier).Value);

        [HttpPost("adopt/{petId:int}")]
        public async Task<ApiResponseDto> AdoptPetAsync(int petId) =>
            await _userPetService.AdoptPetAsync(userId, petId);
        [HttpGet("adoptions")]
        public async Task<ApiResponseDto<PetListDto[]>> GetUserAdoptionPetsAsync() =>
            await _userPetService.GetUserAdoptionPetsAsync(userId);

        [HttpGet("favorites")]
        public async Task<ApiResponseDto<PetListDto[]>> GetUserFavoritePetsAsync() =>
            await _userPetService.GetUserFavoritePetsAsync(userId);

        [HttpPost("favorites/{petid:int}")]
        public async Task<ApiResponseDto> ToggleFavoritesAsync(int petId) =>
            await _userPetService.ToggleFavoritesAsync(userId, petId);
    }
}
