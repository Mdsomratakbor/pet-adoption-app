using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Mobile.ViewModels
{
    public partial class BaseViewModel: ObservableObject
    {
        [ObservableProperty]
        private bool _isBusy;

        public async Task GoToAsync(ShellNavigationState state) =>
            await Shell.Current.GoToAsync(state);

        public async Task GoToAsync(ShellNavigationState state, bool animate) =>
        await Shell.Current.GoToAsync(state, animate);

        public async Task GoToAsync(ShellNavigationState state, IDictionary<string, object> parameters) =>
        await Shell.Current.GoToAsync(state, parameters);

        public async Task GoToAsync(ShellNavigationState state, bool animate ,IDictionary<string, object> parameters) =>
        await Shell.Current.GoToAsync(state, animate, parameters);

        public async Task ShowToastAsync(string message) =>
            await Toast.Make(message).Show();

        public async Task ShowAlertAsync(string title, string message, string buttonText="Ok") =>
            await App.Current.MainPage.DisplayAlert(title, message, buttonText);

        public async Task<bool> ShowConfirmAsync(string title, string message, string okButtonText, string cancelButtonText) =>
          await App.Current.MainPage.DisplayAlert(title, message, okButtonText,cancelButtonText);

        [RelayCommand]
        private async Task GoToDetailsPage(int petId)
        {
            await GoToAsync($"{nameof(DetailsPage)}?{nameof(DetailsViewModel.PetId)}={petId}");
        }

    }
}
