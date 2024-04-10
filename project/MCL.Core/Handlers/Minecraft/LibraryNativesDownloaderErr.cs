using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class LibraryNativesDownloaderErr
{
    public static bool WindowsClassifierNativesExists(MCLibrary lib)
    {
        if (string.IsNullOrWhiteSpace(lib?.Downloads?.Classifiers?.NativesWindows?.URL))
            return false;

        if (string.IsNullOrWhiteSpace(lib?.Downloads?.Classifiers?.NativesWindows?.SHA1))
            return false;

        return true;
    }

    public static bool LinuxClassifierNativesExists(MCLibrary lib)
    {
        if (string.IsNullOrWhiteSpace(lib?.Downloads?.Classifiers?.NativesLinux?.URL))
            return false;

        if (string.IsNullOrWhiteSpace(lib?.Downloads?.Classifiers?.NativesLinux?.SHA1))
            return false;

        return true;
    }

    public static bool OSXClassifierNativesExists(MCLibrary lib)
    {
        if (string.IsNullOrWhiteSpace(lib?.Downloads?.Classifiers?.NativesMacos?.URL))
            return false;

        if (string.IsNullOrWhiteSpace(lib?.Downloads?.Classifiers?.NativesMacos?.SHA1))
            return false;

        return true;
    }
}
