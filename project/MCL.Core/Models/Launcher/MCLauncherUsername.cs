using MCL.Core.Helpers;

namespace MCL.Core.Models.Launcher;

public class MCLauncherUsername
{
    public string Username { get; set; }

    public string ValidateUsername(int length = 16)
    {
        if (Username.Length > length)
            return Username[..length];
        return Username;
    }

    public string UUID() => CryptographyHelper.UUID(ValidateUsername());
}
