using System.Collections.Generic;
using System.Linq;
using MCL.Core.Java.Models;
using MCL.Core.Launcher.Enums;
using MCL.Core.Launcher.Models;
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

    public static JvmArguments Concat(this JvmArguments jvmArguments, JvmArguments concat)
    {
        List<LaunchArg> arguments = jvmArguments
            .Arguments.Concat(concat.Arguments)
            .GroupBy(arg => arg.Arg)
            .Select(group => group.Last())
            .ToList();

        return new JvmArguments { Arguments = arguments };
    }
}
