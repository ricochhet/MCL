using System.Collections.Generic;

namespace MCL.Core.Models.Minecraft;

public class MCLibrary
{
    public string Name { get; set; }
    public MCLibraryDownloads Downloads { get; set; }
    public List<MCLibraryRule> Rules { get; set; }
}
