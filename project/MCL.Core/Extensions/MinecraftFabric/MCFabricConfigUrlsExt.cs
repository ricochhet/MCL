using MCL.Core.Models.MinecraftFabric;

namespace MCL.Core.Extensions.MinecraftFabric;

public static class MCFabricConfigUrlsExt
{
    public static bool VersionsIndexExists(this MCFabricConfigUrls fabricConfigUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricConfigUrls?.FabricVersionsIndex);
    }

    public static bool LoaderProfileUrlExists(this MCFabricConfigUrls fabricConfigUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricConfigUrls?.FabricLoaderProfileUrl);
    }

    public static bool LoaderJarUrlExists(this MCFabricConfigUrls fabricConfigUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricConfigUrls?.FabricLoaderJarUrl);
    }

    public static bool ApiLoaderNameExists(this MCFabricConfigUrls fabricConfigUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricConfigUrls?.FabricApiLoaderName);
    }

    public static bool ApiIntermediaryNameExists(this MCFabricConfigUrls fabricConfigUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricConfigUrls?.FabricApiIntermediaryName);
    }
}
