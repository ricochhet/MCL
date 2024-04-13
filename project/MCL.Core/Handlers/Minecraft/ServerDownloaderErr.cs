using MCL.Core.Interfaces;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public class ServerDownloaderErr : IErrorHandleItem<MCVersionDetails>
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (string.IsNullOrWhiteSpace(versionDetails?.Downloads?.Server?.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads?.Server?.URL))
            return false;

        return true;
    }
}
