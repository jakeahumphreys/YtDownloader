namespace YtDownloader.Types;

public sealed class DownloadHistory
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
    public string Duration { get; set; }
    public string ThumbnailUrl { get; set; }
    public string LocationOnDisk { get; set; }
    public bool Success { get; set; }
}