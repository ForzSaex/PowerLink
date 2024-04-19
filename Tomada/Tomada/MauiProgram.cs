using Microsoft.Extensions.Logging;
using CommunityToolkit.Maui;
using SkiaSharp.Views.Maui.Controls.Hosting;

namespace Tomada
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiCommunityToolkit()
                .UseSkiaSharp(true)
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                
                
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("JosefinSans-Regular.ttf", "JosefinSansThin");
                    fonts.AddFont("JosefinSans-SemiBold.ttf", "JosefinSansSemiBold");
                    fonts.AddFont("JosefinSans-Bold.ttf", "JosefinSansBold");
                });


#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();


        }
    }
}
