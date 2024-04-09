using System.Collections.Generic;

namespace MCL.Core.Models.Services;

public class Localization
{
    public Dictionary<string, string> Entries { get; set; }

    public Localization()
    {
        Entries ??= [];
        Entries.Add("localization.service", "localization.service");
    }
}
