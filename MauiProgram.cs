using CommunityToolkit.Maui;
using GetHome.Services;
using GetHome.ViewModels;
using GetHome.Views;
using Microsoft.Extensions.Logging;

namespace GetHome;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("Inter-Regular.ttf", "InterRegular");
                fonts.AddFont("Inter-SemiBold.ttf", "InterSemiBold");
                fonts.AddFont("Inter-Bold.ttf", "InterBold");
            });

        // Supprime la bordure native des Entry : le cadre vient du Border de la charte
        Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoBorder", (handler, view) =>
        {
#if ANDROID
            handler.PlatformView.Background = null;
#elif WINDOWS
            handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
        });

        // Services
        builder.Services.AddSingleton<IDataService, MockDataService>();

        // Pages et ViewModels
        builder.Services.AddTransient<LoginPage>();
        builder.Services.AddTransient<LoginViewModel>();

        builder.Services.AddTransient<RegisterPage>();
        builder.Services.AddTransient<RegisterViewModel>();

        builder.Services.AddTransient<ForgotPasswordPage>();
        builder.Services.AddTransient<ForgotPasswordViewModel>();

        builder.Services.AddTransient<AccueilPage>();
        builder.Services.AddTransient<AccueilViewModel>();

        builder.Services.AddTransient<FiltresPage>();
        builder.Services.AddTransient<FiltresViewModel>();

        builder.Services.AddTransient<ResultatsPage>();
        builder.Services.AddTransient<ResultatsViewModel>();

        builder.Services.AddTransient<DetailAnnoncePage>();
        builder.Services.AddTransient<DetailAnnonceViewModel>();

        builder.Services.AddTransient<PublierPage>();
        builder.Services.AddTransient<PublierViewModel>();

        builder.Services.AddTransient<FavorisPage>();
        builder.Services.AddTransient<FavorisViewModel>();
        builder.Services.AddTransient<MessagesPage>();
        builder.Services.AddTransient<MessagesViewModel>();
        builder.Services.AddTransient<ProfilPage>();
        builder.Services.AddTransient<ProfilViewModel>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
