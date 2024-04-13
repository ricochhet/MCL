using MCL.Core.Interfaces;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public class ClientMappingsDownloaderErr : IErrorHandleItem<MCVersionDetails>
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (string.IsNullOrWhiteSpace(versionDetails?.Downloads?.ClientMappings?.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads?.ClientMappings?.URL))
            return false;

        return true;
    }
}
