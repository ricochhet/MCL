using MCL.Core.Minecraft.Models;

namespace MCL.Core.Minecraft.Extensions;

public static class MVersionDetailsExt
{
    public static bool LibrariesExists(this MVersionDetails versionDetails)
    {
        return versionDetails?.Libraries != null && versionDetails.Libraries?.Count > 0;
    }

    public static bool LoggingExists(this MVersionDetails versionDetails)
    {
        return !string.IsNullOrWhiteSpace(versionDetails?.Logging?.Client?.File?.SHA1)
            && !string.IsNullOrWhiteSpace(versionDetails.Logging?.Client?.File?.URL)
            && !string.IsNullOrWhiteSpace(versionDetails.ID);
    }

    public static bool ClientMappingsExists(this MVersionDetails versionDetails)
    {
        return !string.IsNullOrWhiteSpace(versionDetails?.Downloads?.ClientMappings?.SHA1)
            && !string.IsNullOrWhiteSpace(versionDetails.Downloads?.ClientMappings?.URL)
            && !string.IsNullOrWhiteSpace(versionDetails.ID);
    }

    public static bool ClientExists(this MVersionDetails versionDetails)
    {
        return !string.IsNullOrWhiteSpace(versionDetails?.Downloads?.Client?.SHA1)
            && !string.IsNullOrWhiteSpace(versionDetails.Downloads?.Client?.URL)
            && !string.IsNullOrWhiteSpace(versionDetails.ID);
    }

    public static bool ServerMappingsExists(this MVersionDetails versionDetails)
    {
        return !string.IsNullOrWhiteSpace(versionDetails?.Downloads?.ServerMappings?.SHA1)
            && !string.IsNullOrWhiteSpace(versionDetails.Downloads?.ServerMappings?.URL)
            && !string.IsNullOrWhiteSpace(versionDetails.ID);
    }

    public static bool ServerExists(this MVersionDetails versionDetails)
    {
        return !string.IsNullOrWhiteSpace(versionDetails?.Downloads?.Server?.SHA1)
            && !string.IsNullOrWhiteSpace(versionDetails.Downloads?.Server?.URL)
            && !string.IsNullOrWhiteSpace(versionDetails.ID);
    }

    public static bool AssetIndexExists(this MVersionDetails versionDetails)
    {
        return !string.IsNullOrWhiteSpace(versionDetails?.AssetIndex?.SHA1)
            && !string.IsNullOrWhiteSpace(versionDetails.AssetIndex?.URL)
            && !string.IsNullOrWhiteSpace(versionDetails.Assets);
    }
}
