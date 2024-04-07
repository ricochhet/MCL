using System.Collections.Generic;
using MCL.Core.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class VersionHelper
{
    public static JavaRuntimeTypeEnum GetDownloadedMCVersionJava(string minecraftPath, string minecraftVersion)
    {
        MCVersionManifest versionManifest = Json.Read<MCVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(minecraftPath)
        );

        if (versionManifest == null)
            return default;

        MCVersion version = GetVersion(minecraftVersion, versionManifest.Versions);
        if (version == null)
            return default;

        MCVersionDetails versionDetails = Json.Read<MCVersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(minecraftPath, version)
        );

        if (versionDetails == null)
            return default;
        return GenericEnumParser.Parse(versionDetails?.JavaVersion?.Component, JavaRuntimeTypeEnum.JAVA_RUNTIME_GAMMA);
    }

    public static MCVersion GetVersion(string minecraftVersion, List<MCVersion> versions)
    {
        foreach (MCVersion item in versions)
        {
            if (item.ID == minecraftVersion)
                return item;
        }
        return null;
    }

    public static MCFabricInstaller GetFabricVersion(string fabricVersion, List<MCFabricInstaller> installers)
    {
        foreach (MCFabricInstaller item in installers)
        {
            if (item.Version == fabricVersion)
                return item;
        }
        return null;
    }
}
