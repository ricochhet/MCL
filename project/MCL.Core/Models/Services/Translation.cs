using System.Collections.Generic;

namespace MCL.Core.Models.Services;

public class Translation
{
    public Dictionary<string, string> Entries { get; set; }

    public Translation()
    {
        Entries ??= [];
        Entries.Add("translation.en", "English");
        Entries.Add("translation.cn", "Chinese");
    }
}
