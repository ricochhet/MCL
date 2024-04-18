using MCL.Core.MiniCommon.Helpers;

namespace MCL.Core.Launcher.Models;

public class LauncherUsername(string username)
{
    public string Username { get; set; } = username;

    public string ValidateUsername(int length = 16)
    {
        if (Username.Length > length)
            return Username[..length];
        return Username;
    }

    public string UUID() => CryptographyHelper.CreateUUID(ValidateUsername());
}
