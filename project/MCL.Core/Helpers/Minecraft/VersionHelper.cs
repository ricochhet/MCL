using System.Collections.Generic;
using MCL.Core.Enums;
using MCL.Core.MiniCommon;
using MCL.Core.Models.Launcher;
using MCL.Core.Models.Minecraft;
using MCL.Core.Models.MinecraftFabric;
using MCL.Core.Resolvers;
using MCL.Core.Resolvers.Minecraft;

namespace MCL.Core.Helpers.Minecraft;

public static class VersionHelper
{
    public static JavaRuntimeTypeEnum GetDownloadedMCVersionJava(
        MCLauncherPath minecraftPath,
        MCLauncherVersion minecraftVersion
    )
    {
        if (!MCLauncherPath.Exists(minecraftPath))
            return default;

        if (!MCLauncherVersion.Exists(minecraftVersion))
            return default;

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

    public static MCVersion GetVersion(MCLauncherVersion minecraftVersion, List<MCVersion> versions)
    {
        if (!MCLauncherVersion.Exists(minecraftVersion))
            return null;

        foreach (MCVersion item in versions)
        {
            if (item.ID == minecraftVersion.MCVersion)
                return item;
        }
        return null;
    }

    public static MCFabricInstaller GetFabricVersion(
        MCLauncherVersion fabricVersion,
        List<MCFabricInstaller> installers
    )
    {
        if (!MCLauncherVersion.Exists(fabricVersion))
            return null;

        foreach (MCFabricInstaller item in installers)
        {
            if (item.Version == fabricVersion.FabricVersion)
                return item;
        }
        return null;
    }
}
