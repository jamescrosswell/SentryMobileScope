using System.Diagnostics;
using OpenTelemetry;
using OpenTelemetry.Trace;
using Sentry.OpenTelemetry;

namespace SentryMobileScope;

public static class Tracing
{

    static Tracing()
    {
        _tracerProvider = Sdk.CreateTracerProviderBuilder()
            .AddSource(Source.Name) // <-- The name of an activity sources you care about
            .AddHttpClientInstrumentation() // <-- Adds HttpClient telemetry sources
            .AddSentry() // <-- Configure OpenTelemetry to send traces to Sentry
            .Build();
    }
    
    public static readonly ActivitySource Source = new ("SentryMobileScope.MauiProgram");
    private static TracerProvider _tracerProvider;

    public static Activity? SetDisplayName(this Activity? activity, string displayName)
    {
        if (activity is not null)
        {
            activity.DisplayName = displayName;
        }
        return activity;
    }
}