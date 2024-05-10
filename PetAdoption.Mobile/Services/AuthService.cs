using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetAdoption.Mobile.Services
{
    public class AuthService
    {
        private readonly CommonService _commonService;
        private readonly IAuthApi _authApi;

        public AuthService(CommonService commonService, IAuthApi authApi) {
            _commonService = commonService;
            _authApi = authApi;
        }

        public async Task<bool> LoginRegisterAsync(LogingRegisterModel model)
        {
            ApiResponseDto<AuthResponseDto> apiResponse;
            if (model.isNewUser)
            {
                apiResponse = await _authApi.RegisterAsync(new RegisterRequestDto()
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = model.Password
                });
            }
            else
            {

                apiResponse = await _authApi.LoginAsync(new LoginRequestDto()
                {
                    Email = model.Email,
                    Password = model.Password
                });

                if (!apiResponse.IsSuccess)
                {
                    await App.Current.MainPage.DisplayAlert("Error", apiResponse.Message, "Ok");
                    return false;
                }
                var user = new LoggedInUser(apiResponse.Data.UserId, apiResponse.Data.Name, apiResponse.Data.Token);
                SetUesr(user);
                _commonService.SetToken(apiResponse.Data.Token);
            }
            return true;
        }

        private void SetUesr(LoggedInUser user) =>
             Preferences.Default.Set(UIConstants.UserInfo, user.ToJson());
        public  void LogoutAsync()
        {
            _commonService.SetToken(null);
            Preferences.Default.Remove(UIConstants.UserInfo);
        }
        public LoggedInUser? GetUser()
        {
            var userJson = Preferences.Default.Get(UIConstants.UserInfo, string.Empty);
            return LoggedInUser.LoadFromJson(userJson);
        }
        public bool IsLoggedIn => Preferences.Default.ContainsKey(UIConstants.UserInfo);
    }
}
