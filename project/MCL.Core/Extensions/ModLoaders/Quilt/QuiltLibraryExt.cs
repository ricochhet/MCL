using MCL.Core.Models.ModLoaders.Quilt;

namespace MCL.Core.Extensions.ModLoaders.Quilt;

public static class QuiltLibraryExt
{
    public static bool LibraryExists(this QuiltLibrary library)
    {
        return !string.IsNullOrWhiteSpace(library?.Name) && !string.IsNullOrWhiteSpace(library.URL);
    }
}
