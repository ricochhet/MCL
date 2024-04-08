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
        MCLauncherVersion minecraftVersion,
        JavaRuntimeTypeEnum fallback
    )
    {
        if (!MCLauncherPath.Exists(minecraftPath))
            return fallback;

        if (!MCLauncherVersion.Exists(minecraftVersion))
            return fallback;

        MCVersionManifest versionManifest = Json.Read<MCVersionManifest>(
            MinecraftPathResolver.DownloadedVersionManifestPath(minecraftPath)
        );

        if (versionManifest == null)
            return fallback;

        MCVersion version = GetVersion(minecraftVersion, versionManifest.Versions);
        if (version == null)
            return fallback;

        MCVersionDetails versionDetails = Json.Read<MCVersionDetails>(
            MinecraftPathResolver.DownloadedVersionDetailsPath(minecraftPath, version)
        );

        if (versionDetails == null)
            return fallback;
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

    public static MCFabricInstaller GetFabricInstallerVersion(
        MCLauncherVersion fabricInstallerVersion,
        List<MCFabricInstaller> installers
    )
    {
        if (!MCLauncherVersion.Exists(fabricInstallerVersion))
            return null;

        foreach (MCFabricInstaller item in installers)
        {
            if (item.Version == fabricInstallerVersion.FabricInstallerVersion)
                return item;
        }
        return null;
    }

    public static MCFabricLoader GetFabricLoaderVersion(
        MCLauncherVersion fabricLoaderVersion,
        List<MCFabricLoader> loaders
    )
    {
        if (!MCLauncherVersion.Exists(fabricLoaderVersion))
            return null;

        foreach (MCFabricLoader item in loaders)
        {
            if (item.Version == fabricLoaderVersion.FabricLoaderVersion)
                return item;
        }
        return null;
    }
}
