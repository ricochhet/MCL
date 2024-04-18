using MCL.Core.ModLoaders.Quilt.Models;

namespace MCL.Core.ModLoaders.Quilt.Extensions;

public static class QuiltLibraryExt
{
    public static bool LibraryExists(this QuiltLibrary library)
    {
        return !string.IsNullOrWhiteSpace(library?.Name) && !string.IsNullOrWhiteSpace(library.URL);
    }
}
