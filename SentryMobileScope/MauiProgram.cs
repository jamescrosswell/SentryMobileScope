using Microsoft.Extensions.Logging;

namespace SentryMobileScope;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseSentry(options =>
            {
                // The DSN is the only required setting.
                options.Dsn =
                    "https://27ca1597850dd871875d6a2a2bdf2666@o4507606271524864.ingest.us.sentry.io/4507810958278656";
                options.Debug = true;
                options.TracesSampleRate = 1.0;
            })
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}