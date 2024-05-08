using PetAdoption.Api.Data.Entities;
using PetAdoption.Shared.Dtos;

namespace PetAdoption.Api.Extentions
{
    public static class Mappers
    {
        public static PetDetailDto MapToPetDetailsDto(this Pet pet) =>
            new PetDetailDto()
            {
                AdoptionStatus = pet.AdoptionStatus,
                Breed = pet.Breed,
                DateOfBirth = pet.DateOfBirth,
                Description = pet.Description,
                Gender = pet.Gender,
                Id = pet.Id,
                Image = pet.Image,
                Price = pet.Price,
                Name = pet.Name
            };
    }
}
