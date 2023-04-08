namespace YtDownloader.Types;

public sealed class DownloadHistory
{
    public Guid Id { get; set; }
    public string Url { get; set; }
    public bool Success { get; set; }
}