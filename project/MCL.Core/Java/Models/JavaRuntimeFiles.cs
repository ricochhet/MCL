using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCL.Core.Java.Models;

public class JavaRuntimeFiles
{
    [JsonPropertyName("files")]
    public Dictionary<string, JavaRuntimeFile> Files { get; set; }

    public JavaRuntimeFiles() { }
}
