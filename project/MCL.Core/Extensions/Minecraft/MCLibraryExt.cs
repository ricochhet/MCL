using MCL.Core.Models.Minecraft;

namespace MCL.Core.Extensions.Minecraft;

public static class MCLibraryExt
{
    public static bool ArtifactExists(this MCLibrary lib)
    {
        return !string.IsNullOrWhiteSpace(lib?.Downloads?.Artifact?.Path)
            && !string.IsNullOrWhiteSpace(lib.Downloads?.Artifact?.URL)
            && !string.IsNullOrWhiteSpace(lib.Downloads?.Artifact?.SHA1);
    }

    public static bool WindowsClassifierNativesExists(this MCLibrary lib)
    {
        return !string.IsNullOrWhiteSpace(lib?.Downloads?.Classifiers?.NativesWindows?.URL)
            && !string.IsNullOrWhiteSpace(lib.Downloads?.Classifiers?.NativesWindows?.SHA1)
            && !string.IsNullOrWhiteSpace(lib.Downloads?.Classifiers?.NativesWindows?.Path);
    }

    public static bool LinuxClassifierNativesExists(this MCLibrary lib)
    {
        return !string.IsNullOrWhiteSpace(lib?.Downloads?.Classifiers?.NativesLinux?.URL)
            && !string.IsNullOrWhiteSpace(lib.Downloads?.Classifiers?.NativesLinux?.SHA1)
            && !string.IsNullOrWhiteSpace(lib.Downloads?.Classifiers?.NativesLinux?.Path);
    }

    public static bool OSXClassifierNativesExists(this MCLibrary lib)
    {
        return !string.IsNullOrWhiteSpace(lib?.Downloads?.Classifiers?.NativesMacos?.URL)
            && !string.IsNullOrWhiteSpace(lib.Downloads?.Classifiers?.NativesMacos?.SHA1)
            && !string.IsNullOrWhiteSpace(lib.Downloads?.Classifiers?.NativesMacos?.Path);
    }
}