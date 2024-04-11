using MCL.Core.Helpers;

namespace MCL.Core.Models.Launcher;

public class MCLauncherUsername(string username)
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
