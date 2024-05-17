using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Mobile.ViewModels
{
    public partial class MyAdoptionsViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        public readonly IUserApi _userApi;
        public MyAdoptionsViewModel(AuthService authService, IUserApi userApi)
        {
            _authService = authService;
            _userApi = userApi;
        }

        [ObservableProperty]
        private IEnumerable<PetListDto> _myAdoptions = Enumerable.Empty<PetListDto>();
        

        public async Task InitializeAsync()
        {
            if (!_authService.IsLoggedIn)
            {
                await ShowToastAsync("You need to be logged in to see your adoptions");
                return;
            }
            try
            {
                IsBusy = true;
                await Task.Delay(100);
                var apiResponse = await _userApi.GetUserAdoptionPetsAsync();
                if (apiResponse.IsSuccess)
                {
                    MyAdoptions = apiResponse.Data;
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
            finally{
                IsBusy = false;
            }
        }
    }
}
