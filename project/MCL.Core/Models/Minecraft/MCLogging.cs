using System.Text.Json.Serialization;

namespace MCL.Core.Models.Minecraft;

public class MCLogging
{
    [JsonPropertyName("client")]
    public MCLoggingClient Client { get; set; }

    public MCLogging(MCLoggingClient client)
    {
        Client = client;
    }
}
