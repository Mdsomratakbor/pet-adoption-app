﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PetAdoption.Api.Services;
using PetAdoption.Api.Services.Interfaces;
using PetAdoption.Shared.Dtos;
using System.Security.Claims;

namespace PetAdoption.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserPetService _userPetService;
        private readonly IPetService _petService;
        private readonly IAuthService _authService;

        public UserController(IUserPetService userPetService, IPetService petService, IAuthService authService)
        {
            _userPetService = userPetService;
            _petService = petService;
            _authService = authService;
        }

        /// HAKE: THERE HAVE A ISSUE
        private int userId => Convert.ToInt32(User.Claims.First(c=>c.Type == ClaimTypes.NameIdentifier).Value);

        [HttpPost("adopt/{petId:int}")]
        public async Task<ApiResponse> AdoptPetAsync(int petId) =>
            await _userPetService.AdoptPetAsync(userId, petId);
        [HttpGet("adoptions")]
        public async Task<ApiResponseDto<PetListDto[]>> GetUserAdoptionPetsAsync() =>
            await _userPetService.GetUserAdoptionPetsAsync(userId);

        [HttpGet("favorites")]
        public async Task<ApiResponseDto<PetListDto[]>> GetUserFavoritePetsAsync() =>
            await _userPetService.GetUserFavoritePetsAsync(userId);

        [HttpPost("favorites/{petid:int}")]
        public async Task<ApiResponse> ToggleFavoritesAsync(int petId) =>
            await _userPetService.ToggleFavoritesAsync(userId, petId);

        [HttpGet("view-pet-details/{petId:int}")]
        public async Task<ApiResponseDto<PetDetailDto>> GetPetsDetailsAsync(int petId) =>
       await _petService.GetPetsDetailsAsync(petId, userId);

        [HttpPost("change-password")]
        public async Task<ApiResponse> ChangePassword(SingleValueDto<string> newPassword)=>
           await _authService.ChangePasswordAsync(userId, newPassword.Value);
    }
}
