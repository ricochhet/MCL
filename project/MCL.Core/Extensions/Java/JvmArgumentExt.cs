using MCL.Core.Enums;
using MCL.Core.Enums.MinecraftFabric;
using MCL.Core.Enums.MinecraftQuilt;
using MCL.Core.Models.Java;

namespace MCL.Core.Extensions.Java;

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
