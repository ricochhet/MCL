using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public static class LibraryDownloaderErr
{
    public static bool Exists(MCLibrary lib)
    {
        if (string.IsNullOrWhiteSpace(lib?.Downloads?.Artifact?.Path))
            return false;

        if (string.IsNullOrWhiteSpace(lib.Downloads?.Artifact?.URL))
            return false;

        if (string.IsNullOrWhiteSpace(lib.Downloads?.Artifact?.SHA1))
            return false;

        return true;
    }
}
