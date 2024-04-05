using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using MCL.Core.Providers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class ClassPathHelper
{
    public static string CreateClassPath(string minecraftPath, string minecraftVersion)
    {
        string separator = Environment.OSVersion.Platform switch
        {
            PlatformID.Unix or PlatformID.MacOSX => ":",
            PlatformID.Win32NT => ";",
            _ => throw new Exception("Unsupported OS."),
        };
        string libPath = Path.Combine(minecraftPath, "libraries");
        List<string> libraries = FsProvider.GetFiles(libPath, "*");
        libraries = libraries.Prepend(MinecraftArgsResolver.ClientLibrary(minecraftVersion)).ToList();

        return string.Join(separator, libraries.Select(lib => lib.Replace(minecraftPath, "").Replace("\\", "/")));
    }
}
