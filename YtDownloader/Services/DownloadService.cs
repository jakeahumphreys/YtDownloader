using YoutubeExplode;
using YoutubeExplode.Converter;
using YtDownloader.Common;

namespace YtDownloader.Services;

public interface IDownloadService
{
    public Task SaveAudioToDisk(string url);
}

public sealed class DownloadService : IDownloadService
{
    private readonly YoutubeClient _youtubeClient;

    public DownloadService(YoutubeClient youtubeClient)
    {
        _youtubeClient = youtubeClient;
    }

    public async Task SaveAudioToDisk(string url)
    {
        await _youtubeClient.Videos.DownloadAsync(url, $"{FilePathHelper.ExportDirectory}\\Download {DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss")}.mp3", options => options
            .SetContainer("mp4")
            .SetFFmpegPath(FilePathHelper.FfmPegExe));
    }
}