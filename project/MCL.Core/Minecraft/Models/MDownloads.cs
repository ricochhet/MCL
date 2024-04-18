using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MDownloads(MDownload client, MDownload clientMappings, MDownload server, MDownload serverMappings)
{
    [JsonPropertyName("client")]
    public MDownload Client { get; set; } = client;

    [JsonPropertyName("client_mappings")]
    public MDownload ClientMappings { get; set; } = clientMappings;

    [JsonPropertyName("server")]
    public MDownload Server { get; set; } = server;

    [JsonPropertyName("server_mappings")]
    public MDownload ServerMappings { get; set; } = serverMappings;
}
