using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Helpers.MinecraftFabric;

#pragma warning disable IDE0079
#pragma warning disable S2436
public interface IMCFabricVersionHelper<out T1, out T2, in T3>
{
    public static abstract T1 GetInstallerVersion(MCLauncherVersion installerVersion, T3 index);

    public static abstract T2 GetLoaderVersion(MCLauncherVersion loaderVersion, T3 index);
}
#pragma warning restore
