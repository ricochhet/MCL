using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLogging(MCLoggingClient client)
{
    [JsonPropertyName("client")]
    public MCLoggingClient Client { get; set; } = client;
}
