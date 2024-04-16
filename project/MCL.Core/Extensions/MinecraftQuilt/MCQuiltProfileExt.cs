using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Extensions.MinecraftQuilt;

public static class MCQuiltProfileExt
{
    public static bool LibraryExists(this MCQuiltProfile quiltProfile)
    {
        return quiltProfile?.Libraries != null;
    }
}
