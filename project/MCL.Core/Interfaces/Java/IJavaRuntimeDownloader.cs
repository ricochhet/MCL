using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Java;

public interface IJavaRuntimeDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        JavaRuntimeTypeEnum javaRuntimeType,
        JavaRuntimeFiles javaRuntimeFiles
    );
}
