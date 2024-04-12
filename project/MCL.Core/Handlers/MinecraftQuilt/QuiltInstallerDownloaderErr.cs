using MCL.Core.Models.MinecraftQuilt;

namespace MCL.Core.Web.MinecraftQuilt;

public static class QuiltInstallerDownloaderErr
{
    public static bool Exists(MCQuiltInstaller quiltInstaller)
    {
        if (string.IsNullOrWhiteSpace(quiltInstaller?.URL))
            return false;

        if (string.IsNullOrWhiteSpace(quiltInstaller.Version))
            return false;

        return true;
    }
}
