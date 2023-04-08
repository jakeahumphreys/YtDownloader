using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MudBlazor.Services;
using YoutubeExplode;
using YtDownloader.Services;

namespace YtDownloader;

public static class Startup
{
    public static IServiceProvider? Services { get; private set; }

    public static void Init()
    {
        var host = Host.CreateDefaultBuilder()
            .ConfigureServices(SetupServices)
            .Build();
        Services = host.Services;
    }

    private static void SetupServices(HostBuilderContext context, IServiceCollection services)
    {
        services.AddWpfBlazorWebView();
        services.AddMudServices();
        services.AddSingleton<YoutubeClient>(new YoutubeClient());
        services.AddSingleton<IDownloadService, DownloadService>();

#if DEBUG
        services.AddBlazorWebViewDeveloperTools();
#endif
    }
}