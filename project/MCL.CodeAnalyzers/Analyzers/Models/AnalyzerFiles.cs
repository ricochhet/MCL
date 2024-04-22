using System.Collections.Generic;

namespace MCL.CodeAnalyzers.Analyzers.Models;

public static class AnalyzerFiles
{
    public static List<string> Restricted { get; private set; } =
        ["AssemblyInfo", "AssemblyAttributes", "GlobalSuppressions"];
}
