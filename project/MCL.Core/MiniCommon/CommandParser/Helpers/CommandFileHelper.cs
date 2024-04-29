using System.Collections.Generic;
using System.Linq;
using MCL.Core.MiniCommon.IO;

namespace MCL.Core.MiniCommon.CommandParser.Helpers;

public static class CommandFileHelper
{
    public static Dictionary<string, string> Commands(string filePath)
    {
        string[] lines = VFS.ReadAllLines(filePath);
        Dictionary<string, string> options = [];
        foreach (string line in lines)
            options = options.Concat(CommandLine.ParseKeyValuePairs(line)).ToDictionary();
        return options.GroupBy(a => a).Select(a => a.Last()).ToDictionary();
    }
}
