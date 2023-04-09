using System.Diagnostics;
using System.IO;
using JCommon.ErrorHandling;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using YtDownloader.Common;
using YtDownloader.Services;
using YtDownloader.Types;

namespace YtDownloader;

public partial class Index
{
    [Inject] IDownloadService _downloadService { get; set; }
    
    public List<Error> Errors { get; set; }
    public List<DownloadHistory> DownloadHistoryItems { get; set; }
    public List<LogItem> LogItems { get; set; }
    public string VideoUrl { get; set; }
    public bool DownloadsEnabled { get; set; }

    protected override Task OnInitializedAsync()
    {
        Errors = new List<Error>();
        DownloadHistoryItems = new List<DownloadHistory>();
        LogItems = new List<LogItem>();
        DownloadsEnabled = true;

        if (!IsFfmPegPresent())
        {
            DownloadsEnabled = false;
            Errors.Add(new Error($"Ffmpeg.exe is not present at {FilePathHelper.FfmPegExe}, downloads are not possible at this time."));
            StateHasChanged();
        }

        
        return base.OnInitializedAsync();
    }

    private void AddLog(string icon, Color iconColour, string message)
    {
        LogItems.Add(new LogItem
        {
            OccurredAt = DateTime.Now,
            Icon = icon,
            IconColour = iconColour,
            Message = message
        });
        StateHasChanged();
    }

    private void ShowFile(string path)
    {
        Process.Start("explorer.exe", $"/select,{path}");
    }

    private async Task CallDownload()
    {
        AddLog(@Icons.Material.Filled.Info, Color.Info  ,$"Attempting to download {VideoUrl}");
        await DownloadVideoFromUrl();
    }

    private async Task DownloadVideoFromUrl()
    {
        if (!string.IsNullOrWhiteSpace(VideoUrl))
        {
            var downloadResult = await _downloadService.SaveAudioToDisk(VideoUrl);
            
            if (downloadResult.IsFailure)
            {
                AddLog(Icons.Material.Filled.Error, Color.Error,$"Download failed: {downloadResult.Errors.First().Message}");
                StateHasChanged();
                return;
            }
            
            AddLog(Icons.Material.Filled.Check, Color.Success,$"Downloaded {downloadResult.Content.Title} successfully!");
            DownloadHistoryItems.Add(downloadResult.Content);
            StateHasChanged();
        }
    }

    private bool IsFfmPegPresent()
    {
        return File.Exists(FilePathHelper.FfmPegExe);
    }
}