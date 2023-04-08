using YoutubeExplode;
using YoutubeExplode.Converter;
using YtDownloader.Common;
using YtDownloader.Types;

namespace YtDownloader.Services;

public interface IDownloadService
{
    public Task<DownloadHistory> SaveAudioToDisk(string url);
}

public sealed class DownloadService : IDownloadService
{
    private readonly YoutubeClient _youtubeClient;

    public DownloadService(YoutubeClient youtubeClient)
    {
        _youtubeClient = youtubeClient;
    }

    public async Task<DownloadHistory> SaveAudioToDisk(string url)
    {
        var downloadId = Guid.NewGuid();
        try
        {
            await _youtubeClient.Videos.DownloadAsync(url, $"{FilePathHelper.ExportDirectory}\\Download {DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss")}.mp3", options => options
                .SetContainer("mp4")
                .SetFFmpegPath(FilePathHelper.FfmPegExe));

            return new DownloadHistory
            {
                Id = downloadId,
                Success = true,
                Url = url
            };
        }
        catch (Exception exception)
        {
            return new DownloadHistory
            {
                Id = downloadId,
                Url = url,
                Success = false
            };
        }
        
    }
}