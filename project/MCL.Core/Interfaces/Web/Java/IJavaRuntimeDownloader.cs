using System.Threading.Tasks;
using MCL.Core.Enums.Java;
using MCL.Core.Models.Java;
using MCL.Core.Models.Launcher;

namespace MCL.Core.Interfaces.Web.Java;

public interface IJavaRuntimeDownloader
{
    public static abstract Task<bool> Download(
        MCLauncherPath launcherPath,
        JavaRuntimeType javaRuntimeType,
        JavaRuntimeFiles javaRuntimeFiles
    );
}
