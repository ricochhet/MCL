using MCL.Core.Enums;
using MCL.Core.Models;
using MCL.Core.Models.Java;

namespace MCL.Core.Extensions;

public static class JvmArgumentExt
{
    public static void Add(this JvmArguments jvmArguments, ClientType clientType, LaunchArg launchArg)
    {
        switch (clientType)
        {
            case ClientType.VANILLA:
                jvmArguments.Add(launchArg);
                break;
            case ClientType.FABRIC:
                jvmArguments.Add(launchArg);
                break;
        }
    }

    public static void Add(this JvmArguments jvmArguments, LauncherType launcherType, LaunchArg launchArg)
    {
        switch (launcherType)
        {
            case LauncherType.RELEASE:
                jvmArguments.Add(launchArg);
                break;
            case LauncherType.DEBUG:
                jvmArguments.Add(launchArg);
                break;
        }
    }
}
