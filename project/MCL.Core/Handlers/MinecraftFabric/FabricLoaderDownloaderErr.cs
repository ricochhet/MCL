using MCL.Core.Interfaces;
using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Handlers.MinecraftFabric;

public class FabricLoaderDownloaderErr
    : IErrorHandleItems<MCFabricProfile, MCFabricConfigUrls>,
        IErrorHandleItem<MCFabricLibrary>
{
    public static bool Exists(MCFabricProfile fabricProfile, MCFabricConfigUrls fabricConfigUrls)
    {
        if (string.IsNullOrWhiteSpace(fabricConfigUrls?.FabricLoaderJarUrl))
            return false;

        if (fabricProfile?.Libraries == null)
            return false;

        return true;
    }

    public static bool Exists(MCFabricLibrary library)
    {
        if (string.IsNullOrWhiteSpace(library.Name))
            return false;

        if (string.IsNullOrWhiteSpace(library.URL))
            return false;

        return true;
    }
}
