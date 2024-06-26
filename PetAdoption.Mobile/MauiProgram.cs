﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using PetAdoption.Shared;

namespace PetAdoption.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("Ubuntu-Regular.ttf", "UbuntuRegular");
                    fonts.AddFont("Ubuntu-Bold.ttf", "UbuntuBold");
                })
                .UseMauiCommunityToolkit();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            RegisterAppDependencies(builder.Services);
            ConfigureRefit(builder.Services);
            return builder.Build();
        }

        static void RegisterAppDependencies(IServiceCollection services)
        {
            services.AddTransient<LoginRegisterViewModel>()
                .AddTransient<LoginRegistrationPage>();

            services.AddSingleton<HomeViewModel>()
                .AddSingleton<HomePage>();

            services.AddSingleton<AllPetsViewModel>()
                .AddTransient<AllPetsPage>();

            services.AddTransient<ProfileViewModel>()
                .AddTransient<ProfilePage>();
            services.AddTransient<FavoritesViewModel>()
                .AddTransient<FavoritesPage>();

            services.AddTransientWithShellRoute<DetailsPage, DetailsViewModel>(nameof(DetailsPage));

            services.AddTransientWithShellRoute<AdoptionsPage,MyAdoptionsViewModel>(nameof(AdoptionsPage));

            services.AddTransient<AuthService>();

            services.AddSingleton<CommonService>();
        }

        static void ConfigureRefit(IServiceCollection services)
        {
            services.AddRefitClient<IAuthApi>()
                .ConfigureHttpClient(SetHttpClient);

            services.AddRefitClient<IPetsApi>()
               .ConfigureHttpClient(SetHttpClient);

            services.AddRefitClient<IUserApi>(sp =>
            {
                string token = string.Empty;
                var commonService = sp.GetService<CommonService>();

               
                return new()
                {
                    AuthorizationHeaderValueGetter = (_, __) => Task.FromResult(commonService?.Token ?? token)
                };
            })
              .ConfigureHttpClient(SetHttpClient);

            static void SetHttpClient(HttpClient httpClient) =>
                httpClient.BaseAddress = new Uri(AppConstants.BaseApiUrl);
        }
    }
}
