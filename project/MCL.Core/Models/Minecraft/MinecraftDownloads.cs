using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftDownloads(
    MinecraftDownload client,
    MinecraftDownload clientMappings,
    MinecraftDownload server,
    MinecraftDownload serverMappings
)
{
    [JsonPropertyName("client")]
    public MinecraftDownload Client { get; set; } = client;

    [JsonPropertyName("client_mappings")]
    public MinecraftDownload ClientMappings { get; set; } = clientMappings;

    [JsonPropertyName("server")]
    public MinecraftDownload Server { get; set; } = server;

    [JsonPropertyName("server_mappings")]
    public MinecraftDownload ServerMappings { get; set; } = serverMappings;
}
