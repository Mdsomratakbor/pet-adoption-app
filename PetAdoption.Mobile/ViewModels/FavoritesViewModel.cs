using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Mobile.ViewModels
{
    public partial class FavoritesViewModel : BaseViewModel
    {
        private readonly IUserApi _userApi;
        private readonly AuthService _authService;

        public FavoritesViewModel(IUserApi userApi, AuthService authService)
        {
            _userApi = userApi;
            _authService = authService;
        }

        [ObservableProperty]
        public ObservableCollection<PetSlim> _pets  = new();

        public async Task InitializeAsync()
        {
            if (!_authService.IsLoggedIn)
            {
                await ShowToastAsync("You need to logged in to load your favorite pets");
                return;
            }

            try
            {
                IsBusy = true;
                var apiResponse = await _userApi.GetUserFavoritePetsAsync();
                if (apiResponse.IsSuccess)
                {
                    Pets = new ObservableCollection<PetSlim>
                        (apiResponse.Data.Select(p => new PetSlim()
                    {
                        Id = p.Id,
                        Image = p.Image,
                        Name = p.Name,
                        IsFavorite = true
                    }));
                }
                else
                {
                    await ShowAlertAsync("Error in fetching pets", apiResponse?.Message);
                }
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Error in fetching pets", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task ToggleFavoriteAsync(int petId)
        {
            try
            {

            
                var pet = Pets.FirstOrDefault(p => p.Id == petId);
                if(pet is not null)
                {
                    IsBusy = true;
                    pet.IsFavorite =  false;
                    await _userApi.ToggleFavoritesAsync(petId);

                    Pets.Remove(pet);
                }
                   
            }
            catch(Exception ex)
            {
                await ShowAlertAsync("Error in toggle pets", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
