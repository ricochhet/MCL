using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Resolvers.MinecraftFabric;

public interface IMinecraftFabricPathResolver<in T>
{
    public static abstract string InstallerPath(MCLauncherPath launcherPath);

    public static abstract string ModPath(MCLauncherPath launcherPath);

    public static abstract string ModCategoryPath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion);

    public static abstract string DownloadedInstallerPath(
        MCLauncherPath launcherPath,
        MCLauncherVersion launcherVersion
    );

    public static abstract string DownloadedIndexPath(MCLauncherPath launcherPath);

    public static abstract string DownloadedProfilePath(MCLauncherPath launcherPath, MCLauncherVersion launcherVersion);

    public static abstract string LoaderJarUrlPath(T configUrls, MCLauncherVersion launcherVersion);

    public static abstract string LoaderProfileUrlPath(T configUrls, MCLauncherVersion launcherVersion);
}
