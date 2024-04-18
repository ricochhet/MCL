using MCL.Core.Models.ModLoaders.Fabric;

namespace MCL.Core.Extensions.ModLoaders.Fabric;

public static class FabricLibraryExt
{
    public static bool LibraryExists(this FabricLibrary library)
    {
        return !string.IsNullOrWhiteSpace(library?.Name) && !string.IsNullOrWhiteSpace(library.URL);
    }
}
