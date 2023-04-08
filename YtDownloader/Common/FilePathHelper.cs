using System;
using System.IO;

namespace YtDownloader.Common;

public static class FilePathHelper
{
    public static string BaseDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "YtDownloader");
    public static string FfmPegDirectory = $"{BaseDirectory}\\ffmpeg";
    public static string FfmPegExe = $"{FfmPegDirectory}\\ffmpeg.exe";
    public static string ExportDirectory = $"{BaseDirectory}";
}