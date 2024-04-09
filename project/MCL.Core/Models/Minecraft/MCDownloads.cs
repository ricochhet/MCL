using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCDownloads(MCDownload client, MCDownload clientMappings, MCDownload server, MCDownload serverMappings)
{
    [JsonPropertyName("client")]
    public MCDownload Client { get; set; } = client;

    [JsonPropertyName("client_mappings")]
    public MCDownload ClientMappings { get; set; } = clientMappings;

    [JsonPropertyName("server")]
    public MCDownload Server { get; set; } = server;

    [JsonPropertyName("server_mappings")]
    public MCDownload ServerMappings { get; set; } = serverMappings;
}
