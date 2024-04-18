using MCL.Core.Models.ModLoaders.Fabric;

namespace MCL.Core.Extensions.ModLoaders.Fabric;

public static class FabricProfileExt
{
    public static bool LibraryExists(this FabricProfile fabricProfile)
    {
        return fabricProfile?.Libraries != null;
    }
}
