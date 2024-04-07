using System.Threading.Tasks;
using MCL.Core.Enums;
using MCL.Core.Models.Java;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Interfaces.Java;

public interface IJavaRuntimeDownloader
{
    public static abstract Task<bool> Download(
        string minecraftPath,
        JavaRuntimeTypeEnum javaRuntimeType,
        JavaRuntimeFiles javaRuntimeFiles
    );
    public static abstract bool Exists(string minecraftPath, JavaRuntimeFiles javaRuntimeFiles);
}
