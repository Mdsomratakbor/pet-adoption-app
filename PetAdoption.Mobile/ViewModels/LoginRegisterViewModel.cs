namespace PetAdoption.Mobile.ViewModels
{
    [QueryProperty(nameof(IsFirstTime), nameof(IsFirstTime))]
    public partial class LoginRegisterViewModel: BaseViewModel
    {
        [ObservableProperty]
        private bool _isRegistrationMode;

        [ObservableProperty]
        private LogingRegisterModel _model = new();


        [ObservableProperty]
        private bool _isFirstTime;
       
        partial void OnIsFirstTimeChanging(bool value)
        {

            if (value)
                IsRegistrationMode = true;
        }

        [RelayCommand]
        private void ToggleMode() => IsRegistrationMode = !IsRegistrationMode;

        [RelayCommand]
        private async Task SkipForNow() =>
            await GoToAsync($"//{nameof(HomePage)}");



        [RelayCommand]
        private async Task Submit()
        {
            if (!Model.Validate(IsRegistrationMode))
            {
                await ShowToastAsync("All fields are mendetory");
                return;
            }

            IsBusy = true;
            await Task.Delay(1000);
            await SkipForNow();
        }



    }
}
