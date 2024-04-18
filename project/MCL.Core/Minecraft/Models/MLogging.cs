using System.Text.Json.Serialization;

namespace MCL.Core.Minecraft.Models;

public class MLogging(MLoggingClient client)
{
    [JsonPropertyName("client")]
    public MLoggingClient Client { get; set; } = client;
}
