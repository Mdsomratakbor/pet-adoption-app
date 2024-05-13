using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Mobile.ViewModels
{
    public partial class AllPetsViewModel : BaseViewModel
    {
        private readonly IPetsApi _petsApi;

        public AllPetsViewModel(IPetsApi petsApi)

        {
            _petsApi = petsApi;
        }

        [ObservableProperty]
        private IEnumerable<PetListDto> _pets = Enumerable.Empty<PetListDto>();

        [ObservableProperty]
        private bool _isRefreshing;

        private bool _isInitialized;

        public async Task InitializeAsync()
        {
            if (_isInitialized)
                return;
            _isInitialized = true;
            await LoadAllPets(true);

        }

        private async Task LoadAllPets(bool initialLoad)
        {
            if (initialLoad)
                IsBusy = true;
            else
                IsRefreshing = true;
            await Task.Delay(3200);
            try
            {
                var apiResponse = await _petsApi.GetAllPetsAsync();
                if(apiResponse.IsSuccess)
                {
                    Pets = apiResponse.Data;
                }
                else
                {
                    await ShowAlertAsync("Error in loding pets", apiResponse.Message);
                }
            }
            catch(Exception ex)
            {
                await ShowAlertAsync("Error in loading pets", ex.Message);
            }
            finally
            {
                IsBusy = IsRefreshing= false;
            }
        }

        [RelayCommand]
        private async Task LoadPets() => await LoadAllPets(false);
    }
}
