
using PetAdoption.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Mobile.ViewModels
{
    [QueryProperty(nameof(PetId), nameof(PetId))]
    public partial class DetailsViewModel: BaseViewModel
    {
        private readonly IPetsApi _petsApi;
        private readonly AuthService _authService;
        private readonly IUserApi _userApi;

        public DetailsViewModel(IPetsApi petsApi, AuthService authService, IUserApi userApi) {
            _petsApi = petsApi;
            _authService = authService;
            _userApi = userApi;
        }
        [ObservableProperty]
        private int _petId;

        [ObservableProperty]
        private Pet _petDetail = new();


        async partial  void OnPetIdChanging(int petId)
        {
            IsBusy = true;
            try
            {
                var apiResponse = _authService.IsLoggedIn ?
                    await _userApi.GetPetsDetailsAsync(petId):
                    await _petsApi.GetPetsDetailsAsync(petId);

                if (apiResponse.IsSuccess)
                {
                   var petDto = apiResponse.Data;
                    PetDetail = new()
                    {
                        AdoptionStatus = petDto.AdoptionStatus,
                        Age = petDto.Age,
                        Breed = petDto.Breed,
                        Description = petDto.Description,
                        GenderDisplay = petDto.GenderDisplay,
                        GenderImage = petDto.GenderImage,
                        Id= petDto.Id,
                        Image = petDto.Image,
                        IsFavorite = petDto.IsFavorite,
                        Name = petDto.Name,
                        Price = petDto.Price
                    };
                }
                else
                {
                    await ShowAlertAsync("Error", apiResponse.Message);
                }
            }
            catch (Exception ex)
            {

                await ShowAlertAsync("Error", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task GoBack() => await GoToAsync("..");

        [RelayCommand]
        private async Task ToggleFavorite()
        {
            if (!_authService.IsLoggedIn)
            {
                await ShowToastAsync("You need to be logged in to mark this pet as favorite");
                return;
            }
            PetDetail.IsFavorite = !PetDetail.IsFavorite;
            try
            {
                IsBusy = true;
                await _userApi.ToggleFavoritesAsync(PetId);
                IsBusy = false;
            }
            catch(Exception ex)
            {
                IsBusy = false;
                PetDetail.IsFavorite = !PetDetail.IsFavorite;
                await ShowAlertAsync("Error in toggling favorite status", ex.Message);
            }
        }
    }
}
