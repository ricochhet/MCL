using MCL.Core.Models.ModLoaders.Fabric;

namespace MCL.Core.Extensions.ModLoaders.Fabric;

public static class FabricUrlsExt
{
    public static bool VersionsIndexExists(this FabricUrls fabricUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricUrls?.FabricVersionsIndex);
    }

    public static bool LoaderProfileUrlExists(this FabricUrls fabricUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricUrls?.FabricLoaderProfileUrl);
    }

    public static bool LoaderJarUrlExists(this FabricUrls fabricUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricUrls?.FabricLoaderJarUrl);
    }

    public static bool ApiLoaderNameExists(this FabricUrls fabricUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricUrls?.FabricApiLoaderName);
    }

    public static bool ApiIntermediaryNameExists(this FabricUrls fabricUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricUrls?.FabricApiIntermediaryName);
    }
}
