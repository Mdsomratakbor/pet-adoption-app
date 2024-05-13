using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Mobile.ViewModels
{
    [QueryProperty(nameof(PetId), nameof(PetId))]
    public partial class DetailsViewModel: BaseViewModel
    {
        private readonly IPetsApi _petsApi;
        public DetailsViewModel(IPetsApi petsApi) {
            _petsApi = petsApi;
        }
        [ObservableProperty]
        private int _petId;

        [ObservableProperty]
        private PetDetailDto _petDetail = new PetDetailDto();


        async partial  void OnPetIdChanging(int petId)
        {
            IsBusy = true;
            try
            {
                var apiResponse = await _petsApi.GetPetsDetailsAsync(petId);
                if (apiResponse.IsSuccess)
                {
                    PetDetail = apiResponse.Data;
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
    }
}
