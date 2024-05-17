
using Microsoft.AspNetCore.SignalR.Client;
using PetAdoption.Shared;
using PetAdoption.Shared.HubModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Json;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Mobile.ViewModels
{
    [QueryProperty(nameof(PetId), nameof(PetId))]
    public partial class DetailsViewModel : BaseViewModel, IAsyncDisposable
    {
        private readonly IPetsApi _petsApi;
        private readonly AuthService _authService;
        private readonly IUserApi _userApi;

        private HubConnection _hubConnection;

        public DetailsViewModel(IPetsApi petsApi, AuthService authService, IUserApi userApi)
        {
            _petsApi = petsApi;
            _authService = authService;
            _userApi = userApi;
        }
        [ObservableProperty]
        private int _petId;

        [ObservableProperty]
        private Pet _petDetail = new();


        async partial void OnPetIdChanging(int petId)
        {
            IsBusy = true;
            try
            {
                await ConfigureSignalRHubConnectionAsync(petId);
                var apiResponse = _authService.IsLoggedIn ?
                    await _userApi.GetPetsDetailsAsync(petId) :
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
                        Id = petDto.Id,
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

        private async Task ConfigureSignalRHubConnectionAsync(int currentPetId)
        {
            try
            {
                var _hubConnection = new HubConnectionBuilder()
                            .WithUrl(AppConstants.HubFullUrl)
                            .Build();
                _hubConnection.On<int>(nameof(IPetHubClient.PetIsBeingViewd), async petId =>
                {
                    if (currentPetId == petId)
                    {
                        await App.Current.Dispatcher.DispatchAsync(() => ShowToastAsync("Someone else is also viewing this pet"));
                        ;
                    }
                });

                _hubConnection.On<int>(nameof(IPetHubClient.PetAdopted), async petId =>
                {
                    if (currentPetId == petId)
                    {
                        PetDetail.AdoptionStatus = Shared.Enumerations.AdoptionStatus.Adopted;
                        await App.Current.Dispatcher.DispatchAsync(() => ShowToastAsync("Someone adopted this pet. You wont be able to adopt this now"));
                        ;
                    }
                });
                await _hubConnection.StartAsync();

                await _hubConnection.SendAsync(nameof(IPetHubServer.ViewingThisPet), currentPetId);

            }
            catch
            {
                // Eat out this exception
                // this is not and essential feature for this app
                // if there is some issue with this signalr connection we skip it
                // as the app will work fine without signalr as well

                // future i will send this exception any logger server
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
            catch (Exception ex)
            {
                IsBusy = false;
                PetDetail.IsFavorite = !PetDetail.IsFavorite;
                await ShowAlertAsync("Error in toggling favorite status", ex.Message);
            }
        }


        [RelayCommand]
        private async Task AdoptNowAsync()
        {
            if (!_authService.IsLoggedIn)
            {
                if (await ShowConfirmAsync("Not Logged In", $"You need to be logged in to adopt a pet {Environment.NewLine} Do you want to go to login page?", "Yes", "No"))
                    await GoToAsync($"//{nameof(LoginRegistrationPage)}");
                return;
            }
            IsBusy = true;
            try
            {
                var apiResponse = await _userApi.AdoptPetAsync(PetId);
                if (apiResponse.IsSuccess)
                {
                    PetDetail.AdoptionStatus = Shared.Enumerations.AdoptionStatus.Adopted;
                    if (_hubConnection is not null)
                    {
                        try
                        {
                            await _hubConnection.SendAsync(nameof(IPetHubServer.PetAdopted), PetId);
                        }
                        catch
                        {

                        }
                    }
                    await GoToAsync(nameof(AdoptionSuccessPage));
                }
                else
                {
                    await ShowAlertAsync("Error in adoption", apiResponse.Message);
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                await ShowAlertAsync("Error in adoption", ex.Message);
            }
            finally
            {
                IsBusy = false;
            }
        }

        public async ValueTask DisposeAsync()
        {
            await StopHubConnection();
        }
        public async Task StopHubConnection()
        {
            if (_hubConnection is not null)
            {
                try
                {
                    await _hubConnection.SendAsync(nameof(IPetHubServer.ReleaseViewingThisPet), PetId);
                    await _hubConnection.StopAsync();
                }
                catch (Exception ex)
                {
                    // skip
                }
            }
        }
    }
}
