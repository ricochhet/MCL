using MCL.Core.Interfaces;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public class ClientDownloaderErr : IErrorHandleItem<MCVersionDetails>
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (string.IsNullOrWhiteSpace(versionDetails?.Downloads?.Client?.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads?.Client?.URL))
            return false;

        return true;
    }
}
