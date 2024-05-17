using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Mobile.ViewModels
{
    public partial class ProfileViewModel : BaseViewModel
    {
        private readonly AuthService _authService;
        private readonly CommonService _commonService;
        private readonly IUserApi _userApi;

        public ProfileViewModel(AuthService authService, CommonService commonService, IUserApi userApi)
        {
            _authService = authService;
            _commonService = commonService;
            _userApi = userApi;
            _commonService.LoginStatusChanged += OnLoginStatusChanged; 
            SetUesrInfo();
        }



        private void OnLoginStatusChanged(object? sender, EventArgs e) =>SetUesrInfo();

        private void SetUesrInfo()
        {
            if (_authService.IsLoggedIn)
            {
                var userInfo = _authService.GetUser();
                UserName = userInfo.Name;
                IsLoggedIn = true;
                _commonService.SetToken(userInfo.Token);
            }
            else
            {
                UserName = "Not Logged In";
                IsLoggedIn = false;
            }
        }



        [ObservableProperty, NotifyPropertyChangedFor(nameof(Initials))]
        private string _userName = "Not Logged In";

        [ObservableProperty]
        private bool _isLoggedIn;

        public string Initials
        {
            get
            {
                var parts = UserName.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
                if (parts.Length == 1) // if the username has only one word (e.g. Samrat)
                    return UserName.Length == 1 ?
                        UserName // If username has one word with one character only (e.g. S)
                        : UserName[..2]; // username is one word with multiple characters (e.g. Samrat)

                return $"{parts[0][0]}{parts[1][0]}"; // Username has multiple words (e.g. Md Samrat)
            }
        }

        [RelayCommand]
        private async Task LoginLogoutAsync()
        {
            if (!IsLoggedIn)
            {
                await GoToAsync($"//{nameof(LoginRegistrationPage)}");
            }
            else
            {
                _authService.Logout();
                await GoToAsync($"//{nameof(HomePage)}");
            }
        }

        [RelayCommand]

        private async Task ChangePasswordAsync()
        {
            if (!_authService.IsLoggedIn)
            {
                await ShowToastAsync("You need to be logged in to change your password");
                return;
            }

            var newPassword =await App.Current.MainPage.DisplayPromptAsync("Change Password", "Change Password", placeholder:"Enter new Password");
            if (!string.IsNullOrWhiteSpace(newPassword))
            {
                IsBusy = true;
                await _userApi.ChangePasswordAsync(new SingleValueDto<string>(newPassword));
                IsBusy = false;
                await ShowToastAsync($"Change password successfully");
            }

        }
    }
}
