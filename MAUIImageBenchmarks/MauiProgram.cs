﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace MAUIImageBenchmarks;

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
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("JetBrainsMono[wght].ttf", "JetBrainsMono");
                
            });

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}