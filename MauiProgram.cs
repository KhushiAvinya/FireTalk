using Blazored.LocalStorage;
using FireTalk.Models;
using FireTalk.Services;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace FireTalk
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
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddMudServices();
            builder.Services.AddSingleton<FireStoreService>();
            builder.Services.AddSingleton<IFireTalkService,FireTalkService>();
            builder.Services.AddSingleton<AppState>();
            builder.Services.AddBlazoredLocalStorage();

            //builder.Services.AddSingleton(new HttpClient());

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
