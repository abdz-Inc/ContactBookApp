using ContactBookApp.Model;
using ContactBookApp.View;
using ContactBookApp.ViewModel;
using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using ContactBookApp.Commons.Handlers;

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
            builder.Services.AddTransient<EditContactPage>();
            builder.Services.AddTransient<EditContactViewModel>();
            builder.Services.AddTransient<Model.Contact>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageList>();
            builder.Services.AddSingleton<MainPageViewModel>();
            builder.Services.AddSingleton<ContactBook>();
            builder.Services.AddSingleton<AddContactAlternative>();
            builder.Services.AddSingleton<AddContactAlternativeViewModel>();
            builder.Services.AddSingleton<MainPageSearchHandler>();
            /// builder.Services.AddTransient<AddContactPage>();
            /// builder.Services.AddSingleton<AddContactViewModel>();

#if DEBUG
            builder.Logging.AddDebug();
#endif
            return builder.Build();
        }
    }
}