using Microsoft.EntityFrameworkCore;
using PetAdoption.Api.Data;
using PetAdoption.Api.Data.Entities;
using PetAdoption.Api.Extentions;
using PetAdoption.Api.Services.Interfaces;
using PetAdoption.Shared.Dtos;

namespace PetAdoption.Api.Services
{
    public class UserPetService : IUserPetService
    {
        private static readonly SemaphoreSlim semaphore = new(1, 1);
        private readonly PetContext _context;

        public UserPetService(PetContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseDto> ToggleFavoritesAsync(int userId, int petId)
        {
            try
            {
                var userFavorite = await _context.UserFavorites.FirstOrDefaultAsync(uf => uf.UserId == userId && uf.PetId == petId);
                if (userFavorite is not null)
                {
                    _context.UserFavorites.Remove(userFavorite);
                }
                else
                {
                    userFavorite = new UserFavorites()
                    {
                        UserId = userId,
                        PetId = petId
                    };
                    await _context.UserFavorites.AddAsync(userFavorite);
                }

                await _context.SaveChangesAsync();
                return ApiResponseDto.Success();
            }
            catch (Exception ex)
            {
                return ApiResponseDto.Fail(ex.Message);
            }
        }

        public async Task<ApiResponseDto<PetListDto[]>> GetUserFavoritePetsAsync(int userId)
        {
            var pets = await _context.UserFavorites
                .Where(uf => uf.UserId == userId)
                .Select(uf => uf.Pet)
                .Select(Selectors.PetToPetListDto)
                .ToArrayAsync();
            return ApiResponseDto<PetListDto[]>.Success(pets);
        }

        public async Task<ApiResponseDto<PetListDto[]>> GetUserAdoptionPetsAsync(int userId)
        {
            var pets = await _context.UserAdoption
                .Where(uf => uf.UserId == userId)
                .Select(uf => uf.Pet)
                .Select(Selectors.PetToPetListDto)
                .ToArrayAsync();
            return ApiResponseDto<PetListDto[]>.Success(pets);
        }

        public async Task<ApiResponseDto> AdoptPetAsync(int userId, int petId)
        {
            try
            {
                await semaphore.WaitAsync();
                var pet = await _context.Pets.AsTracking().FirstOrDefaultAsync(p => p.Id == petId);


                if (pet is null)
                {
                    return ApiResponseDto.Fail("Invalid Request");
                }

                if (pet.AdoptionStatus == Shared.Enumerations.AdoptionStatus.Adopted)
                {
                    return ApiResponseDto.Fail($"{pet.Name} is already adopted");
                }

                pet.AdoptionStatus = Shared.Enumerations.AdoptionStatus.Adopted;
                var userAdoption = new UserAdoption
                {
                    UserId = userId,
                    PetId = petId,
                };
                await _context.SaveChangesAsync();
                return ApiResponseDto.Success();
            }
            catch (Exception ex)
            {
                return ApiResponseDto.Fail(ex.Message);
            }
            finally
            {
                semaphore.Release();
            }
        }

    }
}
