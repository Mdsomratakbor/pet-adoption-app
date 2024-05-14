using Microsoft.EntityFrameworkCore;
using PetAdoption.Api.Data;
using PetAdoption.Api.Extentions;
using PetAdoption.Api.Services.Interfaces;
using PetAdoption.Shared.Dtos;

namespace PetAdoption.Api.Services
{
    public class PetService : IPetService
    {
        private readonly PetContext _context;
        public PetService(PetContext context)
        {
            _context = context;
        }

        public async Task<ApiResponseDto<PetListDto[]>> GetNewlyAddedPetsAsync(int count)
        {
            var pets = await _context.Pets.Select(Selectors.PetToPetListDto).OrderByDescending(x => x.Id).Take(count).ToArrayAsync();

            return ApiResponseDto<PetListDto[]>.Success(pets);
        }
        public async Task<ApiResponseDto<PetListDto[]>> GetPopularPetsAsync(int count)
        {
            var pets = await _context.Pets.OrderByDescending(p => p.Views).Take(count).Select(Selectors.PetToPetListDto).ToArrayAsync();
            return ApiResponseDto<PetListDto[]>.Success(pets);
        }

        public async Task<ApiResponseDto<PetListDto[]>> GetRandomPetsAsync(int count)
        {
            var pets = await _context.Pets.OrderByDescending(_ => Guid.NewGuid()).Take(count).Select(Selectors.PetToPetListDto).ToArrayAsync();
            return ApiResponseDto<PetListDto[]>.Success(pets);
        }

        public async Task<ApiResponseDto<PetListDto[]>> GetAllPetsAsync()
        {
            var pets = await _context.Pets.OrderByDescending(p => p.Id).Select(Selectors.PetToPetListDto).ToArrayAsync();
            return ApiResponseDto<PetListDto[]>.Success(pets);
        }

        public async Task<ApiResponseDto<PetDetailDto>> GetPetsDetailsAsync(int petId, int userId = 0)
        {
            var pet = await _context.Pets.AsTracking().FirstOrDefaultAsync(p => p.Id == petId);

            if (pet is not null)
            {

                pet.Views++;
                await _context.SaveChangesAsync();
            }

            var petDto = pet!.MapToPetDetailsDto();

            if (userId > 0)
            {
                if (await _context.UserFavorites.AnyAsync(uf => uf.UserId == userId && uf.PetId == petId))
                    petDto.IsFavorite = true;
            }
            return ApiResponseDto<PetDetailDto>.Success(petDto);
        }
    }
}
