using System.Collections.Generic;

namespace MCL.Core.Helpers;

public class MinecraftArgHelper(
    string _initialHeapSize,
    string _maxHeapSize,
    string _classPath,
    string _mainClass,
    string _username,
    string _userType,
    string _gameDir,
    string _assetIndex,
    string _assetsDir,
    string _uuid,
    string _clientId,
    string _xuid,
    string _accessToken,
    string _version,
    string _versionType,
    List<string> _additionalArguments
)
{
    private readonly string initialHeapSize = _initialHeapSize;
    private readonly string maxHeapSize = _maxHeapSize;
    private readonly string classPath = _classPath;
    private readonly string mainClass = _mainClass;
    private readonly string username = _username;
    private readonly string userType = _userType;
    private readonly string gameDir = _gameDir;
    private readonly string assetIndex = _assetIndex;
    private readonly string assetsDir = _assetsDir;
    private readonly string accessToken = _accessToken;
    private readonly string uuid = _uuid;
    private readonly string clientId = _clientId;
    private readonly string xuid = _xuid;
    private readonly string version = _version;
    private readonly string versionType = _versionType;
    private readonly List<string> additionalArguments = _additionalArguments;

    public string Build()
    {
        return $"-Xms{initialHeapSize}m -Xmx{maxHeapSize}m {string.Join(" ", additionalArguments)} -cp {classPath} {mainClass} "
            + $"--username {username} "
            + $"--userType {userType} "
            + $"--gameDir {gameDir} "
            + $"--assetIndex {assetIndex} "
            + $"--assetsDir {assetsDir} "
            + $"--accessToken {accessToken} "
            + $"--uuid {uuid} "
            + $"--clientId {clientId} "
            + $"--xuid {xuid} "
            + $"--version {version} "
            + $"--versionType {versionType}";
    }
}
