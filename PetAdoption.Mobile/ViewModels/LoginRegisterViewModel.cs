namespace PetAdoption.Mobile.ViewModels
{
    [QueryProperty(nameof(IsFirstTime), nameof(IsFirstTime))]
    public partial class LoginRegisterViewModel: BaseViewModel
    {
       
        public LoginRegisterViewModel(AuthService authService) {
            this.authService = authService;
        }

        [ObservableProperty]
        private bool _isRegistrationMode;

        [ObservableProperty]
        private LogingRegisterModel _model = new();


        [ObservableProperty]
        private bool _isFirstTime;
        private readonly AuthService authService;

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
           var status =  await this.authService.LoginRegisterAsync(Model);
            if (status)
            {
                await SkipForNow();
            }
            IsBusy = false;
        }



    }
}
