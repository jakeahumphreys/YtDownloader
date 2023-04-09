using System.IO;
using JCommon.Communication.Internal;
using YoutubeExplode;
using YoutubeExplode.Converter;
using YtDownloader.Common;
using YtDownloader.Types;

namespace YtDownloader.Services;

public interface IDownloadService
{
    public Task<Result<DownloadHistory>> SaveAudioToDisk(string url);
}

public sealed class DownloadService : IDownloadService
{
    private readonly YoutubeClient _youtubeClient;

    public DownloadService(YoutubeClient youtubeClient)
    {
        _youtubeClient = youtubeClient;
    }

    public async Task<Result<DownloadHistory>> SaveAudioToDisk(string url)
    {
        var downloadId = Guid.NewGuid();
        try
        {
            var video = await _youtubeClient.Videos.GetAsync(url);

            if (!Directory.Exists(FilePathHelper.ExportDirectory))
                Directory.CreateDirectory(FilePathHelper.ExportDirectory);

            var filePath = $"{FilePathHelper.ExportDirectory}\\{video.Title}.mp3";

            await _youtubeClient.Videos.DownloadAsync(url,filePath, options => options
                .SetContainer("mp4")
                .SetFFmpegPath(FilePathHelper.FfmPegExe));

            var thumbnailUrl = video.Thumbnails.First().Url;

            return new Result<DownloadHistory>(new DownloadHistory
            {
                Id = downloadId,
                Title = video.Title,
                Duration = video.Duration.ToString(),
                Author = video.Author.ChannelTitle,
                Url = url,
                ThumbnailUrl = thumbnailUrl,
                LocationOnDisk = filePath
            });
        }
        catch (Exception exception)
        {
            return new Result<DownloadHistory>().WithError(exception.Message);
        }
        
    }
}