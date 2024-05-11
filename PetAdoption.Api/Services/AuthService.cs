using Microsoft.EntityFrameworkCore;
using PetAdoption.Api.Data;
using PetAdoption.Api.Data.Entities;
using PetAdoption.Api.Services.Interfaces;
using PetAdoption.Shared.Dtos;

namespace PetAdoption.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly PetContext _context;
        private readonly TokenService _tokenService;
        public AuthService(PetContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        public async Task<ApiResponseDto<AuthResponseDto>> LoginAsync(LoginRequestDto dto)
        {
            try
            {
                var dbUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

                if (dbUser is null)

                    return ApiResponseDto<AuthResponseDto>.Fail("User does not exist");

                if (dbUser.Password != dto.Password)
                    return ApiResponseDto<AuthResponseDto>.Fail("Incorrect password");

                var token = _tokenService.GenerateJWT(dbUser);

                return ApiResponseDto<AuthResponseDto>.Success(new AuthResponseDto(dbUser.Id, dbUser.Name, token));
            }
            catch (Exception ex)
            {
                return ApiResponseDto<AuthResponseDto>.Fail(ex.Message);
            }
        }


        public async Task<ApiResponseDto<AuthResponseDto>> RegisterAsync(RegisterRequestDto dto)
        {
            try
            {
                var existUser = await _context.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);

                if (existUser is not null)
                    return ApiResponseDto<AuthResponseDto>.Fail("Email already exist");


                var dbUser = new User
                {
                    Email = dto.Email,
                    Name = dto.Name,
                    Password = dto.Password,
                };

                await _context.Users.AddAsync(dbUser);
                await _context.SaveChangesAsync();
                var token = _tokenService.GenerateJWT(dbUser);
                return ApiResponseDto<AuthResponseDto>.Success(new AuthResponseDto(dbUser.Id, dbUser.Name, token));
            }
            catch (Exception ex)
            {
                return ApiResponseDto<AuthResponseDto>.Fail(ex.Message);
            }
        }

    }
}
