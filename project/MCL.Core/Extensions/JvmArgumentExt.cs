using MCL.Core.Enums;
using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Extensions;

public static class JvmArgumentExt
{
    public static void Add(this JvmArguments jvmArguments, ClientType a, ClientType b, LaunchArg launchArg)
    {
        if (a == b)
            jvmArguments.Add(launchArg);
    }

    public static void Add(this JvmArguments jvmArguments, LauncherType a, LauncherType b, LaunchArg launchArg)
    {
        if (a == b)
            jvmArguments.Add(launchArg);
    }

    public static void Add(
        this JvmArguments jvmArguments,
        FabricInstallerType a,
        FabricInstallerType b,
        LaunchArg launchArg
    )
    {
        if (a == b)
            jvmArguments.Add(launchArg);
    }
}
