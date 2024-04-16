using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Extensions.MinecraftQuilt;

public static class MCQuiltLibraryExt
{
    public static bool LibraryExists(this MCQuiltLibrary library)
    {
        return !string.IsNullOrWhiteSpace(library?.Name) && !string.IsNullOrWhiteSpace(library.URL);
    }
}
