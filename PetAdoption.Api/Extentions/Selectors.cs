using PetAdoption.Api.Data.Entities;
using PetAdoption.Shared.Dtos;
using System.Linq.Expressions;

namespace PetAdoption.Api.Extentions
{
    public static class Selectors
    {
        public static Expression<Func<Pet, PetListDto>> PetToPetListDto => p => new PetListDto
        {
            Id = p.Id,
            Name = p.Name,
            Price = p.Price,
            Breed  = p.Breed,
            Image = p.Image
        };
    }
}
