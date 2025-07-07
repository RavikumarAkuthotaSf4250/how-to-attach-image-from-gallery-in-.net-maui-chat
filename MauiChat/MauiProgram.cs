using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
namespace MauiChat
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("MauiMaterialAssets.ttf", "MaterialAssets");
                });

            builder.Services.AddSingleton<ImageDatabase>(_imageDB =>
            {
                var dbPath = Path.Combine(FileSystem.AppDataDirectory, "images.db");
                return new ImageDatabase(dbPath);
            });
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
