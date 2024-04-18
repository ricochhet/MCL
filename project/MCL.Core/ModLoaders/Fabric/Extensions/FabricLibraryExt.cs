using MCL.Core.ModLoaders.Fabric.Models;

namespace MCL.Core.ModLoaders.Fabric.Extensions;

public static class FabricLibraryExt
{
    public static bool LibraryExists(this FabricLibrary library)
    {
        return !string.IsNullOrWhiteSpace(library?.Name) && !string.IsNullOrWhiteSpace(library.URL);
    }
}
