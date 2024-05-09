using PetAdoption.Mobile.Pages;

namespace PetAdoption.Mobile
{
    public partial class MainPage : ContentPage
    {
       

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            // Check if onboarding screen shown

            if (Preferences.Default.ContainsKey(UIConstants.OnboardingShwon))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginRegistrationPage)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(OnboardingPage)}");
            }
        }

    }

}
