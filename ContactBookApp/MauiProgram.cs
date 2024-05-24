using ContactBookApp.Model;
using ContactBookApp.View;
using ContactBookApp.ViewModel;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;

namespace ContactBookApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            }).UseMauiCommunityToolkit();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageList>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<ContactBook>();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}