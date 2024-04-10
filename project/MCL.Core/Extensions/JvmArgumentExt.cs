using MCL.Core.Enums;
using MCL.Core.Models;
using MCL.Core.Models.Java;

namespace MCL.Core.Extensions;

public static class JvmArgumentExt
{
    public static void Add(this JvmArguments jvmArguments, ClientTypeEnum clientType, LaunchArg launchArg)
    {
        switch (clientType)
        {
            case ClientTypeEnum.VANILLA:
                jvmArguments.Add(launchArg);
                break;
            case ClientTypeEnum.FABRIC:
                jvmArguments.Add(launchArg);
                break;
        }
    }

    public static void Add(this JvmArguments jvmArguments, LauncherTypeEnum launcherType, LaunchArg launchArg)
    {
        switch (launcherType)
        {
            case LauncherTypeEnum.RELEASE:
                jvmArguments.Add(launchArg);
                break;
            case LauncherTypeEnum.DEBUG:
                jvmArguments.Add(launchArg);
                break;
        }
    }
}
