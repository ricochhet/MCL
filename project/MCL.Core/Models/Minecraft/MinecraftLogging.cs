using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MinecraftLogging(MinecraftLoggingClient client)
{
    [JsonPropertyName("client")]
    public MinecraftLoggingClient Client { get; set; } = client;
}
