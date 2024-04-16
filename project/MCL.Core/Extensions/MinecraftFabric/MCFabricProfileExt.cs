using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Extensions.MinecraftFabric;

public static class MCFabricProfileExt
{
    public static bool LibraryExists(this MCFabricProfile fabricProfile)
    {
        return fabricProfile?.Libraries != null;
    }
}
