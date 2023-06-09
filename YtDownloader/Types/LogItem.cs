﻿using MudBlazor;

namespace YtDownloader.Types;

public sealed class LogItem
{
    public DateTime OccurredAt { get; set; }
    public string Icon { get; set; }
    public Color IconColour { get; set; }
    public string Message { get; set; }
}