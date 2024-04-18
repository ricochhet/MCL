using MCL.Core.ModLoaders.Fabric.Models;

namespace MCL.Core.ModLoaders.Fabric.Extensions;

public static class FabricProfileExt
{
    public static bool LibraryExists(this FabricProfile fabricProfile)
    {
        return fabricProfile?.Libraries != null;
    }
}
