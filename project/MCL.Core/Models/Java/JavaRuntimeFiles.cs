using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Models.Java;

public class JavaRuntimeFiles
{
    [JsonPropertyName("files")]
    public Dictionary<string, JavaRuntimeFile> Files { get; set; }
}
