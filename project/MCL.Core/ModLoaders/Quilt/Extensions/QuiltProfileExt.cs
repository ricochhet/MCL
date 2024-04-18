using MCL.Core.ModLoaders.Quilt.Models;

namespace MCL.Core.ModLoaders.Quilt.Extensions;

public static class QuiltProfileExt
{
    public static bool LibraryExists(this QuiltProfile quiltProfile)
    {
        return quiltProfile?.Libraries != null;
    }
}
