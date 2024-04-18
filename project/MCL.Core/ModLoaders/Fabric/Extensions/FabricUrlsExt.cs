using MCL.Core.ModLoaders.Fabric.Models;

namespace MCL.Core.ModLoaders.Fabric.Extensions;

public static class FabricUrlsExt
{
    public static bool VersionManifestExists(this FabricUrls fabricUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricUrls?.VersionManifest);
    }

    public static bool LoaderProfileUrlExists(this FabricUrls fabricUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricUrls?.LoaderProfile);
    }

    public static bool LoaderJarUrlExists(this FabricUrls fabricUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricUrls?.LoaderJar);
    }

    public static bool ApiLoaderNameExists(this FabricUrls fabricUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricUrls?.ApiLoaderName);
    }

    public static bool ApiIntermediaryNameExists(this FabricUrls fabricUrls)
    {
        return !string.IsNullOrWhiteSpace(fabricUrls?.ApiIntermediaryName);
    }
}
