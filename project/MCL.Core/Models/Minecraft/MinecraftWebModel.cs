using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class VersionManifest
{
    public Latest Latest { get; set; }
    public List<Version> Versions { get; set; }
}

public class Latest
{
    public string Release { get; set; }
    public string Snapshot { get; set; }
}

public class Version
{
    public string ID { get; set; }
    public string Type { get; set; }
    public string URL { get; set; }
    public string Time { get; set; }
    public string ReleaseTime { get; set; }
}

public class VersionDetails
{
    public Dictionary<string, object> Arguments { get; set; }
    public AssetIndex AssetIndex { get; set; }
    public string Assets { get; set; }
    public int ComplianceLevel { get; set; }
    public Downloads Downloads { get; set; }
    public string ID { get; set; }
    public List<Library> Libraries { get; set; }
    public Logging Logging { get; set; }
    public int MinimumLauncherVersion { get; set; }
    public string ReleaseTime { get; set; }
    public string Time { get; set; }
    public string Type { get; set; }
    public string MainClass { get; set; }
}

public class AssetIndex
{
    public string ID { get; set; }
    public string SHA1 { get; set; }
    public int Size { get; set; }
    public int TotalSize { get; set; }
    public string URL { get; set; }
}

public class Downloads
{
    public DownloadItem Client { get; set; }
    public DownloadItem ClientMappings { get; set; }
    public DownloadItem Server { get; set; }
    public DownloadItem ServerMappings { get; set; }
}

public class DownloadItem
{
    public string SHA1 { get; set; }
    public int Size { get; set; }
    public string URL { get; set; }
}

public class Library
{
    public string Name { get; set; }
    public LibDownloads Downloads { get; set; }
    public List<Rule> Rules { get; set; }
}

public class LibDownloads
{
    public Artifact Artifact { get; set; }
    public Classifiers Classifiers { get; set; }
}

public class Rule
{
    public string Action { get; set; }

    [JsonPropertyName("os")]
    public OS Os { get; set; }
}

public class OS
{
    public string Name { get; set; }
}

public class Classifiers
{
    [JsonPropertyName("natives-linux")]
    public Artifact NativesLinux { get; set; }

    [JsonPropertyName("natives-osx")]
    public Artifact NativesMacos { get; set; }

    [JsonPropertyName("natives-windows")]
    public Artifact NativesWindows { get; set; }
}

public class Artifact
{
    public string Path { get; set; }
    public string SHA1 { get; set; }
    public int Size { get; set; }
    public string URL { get; set; }
}

public class Logging
{
    public LoggingClient Client { get; set; }
}

public class LoggingClient
{
    public string Argument { get; set; }
    public File File { get; set; }
    public string Type { get; set; }
}

public class File
{
    public string ID { get; set; }
    public string SHA1 { get; set; }
    public int Size { get; set; }
    public string URL { get; set; }
}

public class AssetsData
{
    public Dictionary<string, Asset> Objects { get; set; }
}

public class Asset
{
    public string Hash { get; set; }
    public int Size { get; set; }
}
