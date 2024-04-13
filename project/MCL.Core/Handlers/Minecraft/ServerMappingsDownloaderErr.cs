using MCL.Core.Interfaces;
using MCL.Core.Models.Minecraft;

namespace MCL.Core.Handlers.Minecraft;

public class ServerMappingsDownloaderErr : IErrorHandleItem<MCVersionDetails>
{
    public static bool Exists(MCVersionDetails versionDetails)
    {
        if (string.IsNullOrWhiteSpace(versionDetails?.Downloads?.ServerMappings?.SHA1))
            return false;

        if (string.IsNullOrWhiteSpace(versionDetails.Downloads?.ServerMappings?.URL))
            return false;

        return true;
    }
}
