using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCDownloads
{
    [JsonPropertyName("client")]
    public MCDownload Client { get; set; }

    [JsonPropertyName("client_mappings")]
    public MCDownload ClientMappings { get; set; }

    [JsonPropertyName("server")]
    public MCDownload Server { get; set; }

    [JsonPropertyName("server_mappings")]
    public MCDownload ServerMappings { get; set; }
}
