using MCL.Core.Models.ModLoaders.Quilt;

namespace MCL.Core.Extensions.ModLoaders.Quilt;

public static class QuiltProfileExt
{
    public static bool LibraryExists(this QuiltProfile quiltProfile)
    {
        return quiltProfile?.Libraries != null;
    }
}
