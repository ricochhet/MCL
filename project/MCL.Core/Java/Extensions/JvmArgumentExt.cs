using MCL.Core.Java.Models;
using MCL.Core.Launcher.Enums;
using MCL.Core.ModLoaders.Fabric.Enums;
using MCL.Core.ModLoaders.Quilt.Enums;

namespace MCL.Core.Java.Extensions;

public static class JvmArgumentExt
{
    public static void Add(
        this JvmArguments jvmArguments,
        ClientType a,
        ClientType b,
        string arg,
        string[] argParams = null,
        int priority = 0
    )
    {
        if (a == b)
            jvmArguments.Add(arg, argParams, priority);
    }

    public static void Add(
        this JvmArguments jvmArguments,
        LauncherType a,
        LauncherType b,
        string arg,
        string[] argParams = null,
        int priority = 0
    )
    {
        if (a == b)
            jvmArguments.Add(arg, argParams, priority);
    }

    public static void Add(
        this JvmArguments jvmArguments,
        FabricInstallerType a,
        FabricInstallerType b,
        string arg,
        string[] argParams = null,
        int priority = 0
    )
    {
        if (a == b)
            jvmArguments.Add(arg, argParams, priority);
    }

    public static void Add(
        this JvmArguments jvmArguments,
        QuiltInstallerType a,
        QuiltInstallerType b,
        string arg,
        string[] argParams = null,
        int priority = 0
    )
    {
        if (a == b)
            jvmArguments.Add(arg, argParams, priority);
    }
}
