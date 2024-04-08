using System.Text.Json;
using MCL.Core.Providers;

namespace MCL.Core.MiniCommon;

public static class Json
{
    public static T Read<T>(string pathToFile, JsonSerializerOptions options = null)
    {
        if (!FsProvider.Exists(pathToFile))
            return default;

        return JsonSerializer.Deserialize<T>(FsProvider.ReadAllText(pathToFile), options);
    }

    public static void Write(string folderPath, string fileName, object data, JsonSerializerOptions options = null)
    {
        FsProvider.WriteFile(folderPath, fileName, JsonSerializer.Serialize(data, options));
    }

    public static void Write(string folderPath, string fileName, string data, JsonSerializerOptions options = null)
    {
        object deserialized = JsonSerializer.Deserialize<object>(data);
        FsProvider.WriteFile(folderPath, fileName, JsonSerializer.Serialize(deserialized, options));
    }
}
