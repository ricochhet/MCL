using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Extensions.MinecraftFabric;

public static class MCFabricLibraryExt
{
    public static bool LibraryExists(this MCFabricLibrary library)
    {
        return !string.IsNullOrWhiteSpace(library?.Name) && !string.IsNullOrWhiteSpace(library.URL);
    }
}
