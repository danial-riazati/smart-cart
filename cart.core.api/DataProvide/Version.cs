using System;
using System.Collections.Generic;

namespace cart.core.api.DataProvide;

public partial class Version
{
    public string VersionName { get; set; } = null!;

    public string? VersionNumber { get; set; }

    public DateTime? Date { get; set; }

    public string? Url { get; set; }
}
