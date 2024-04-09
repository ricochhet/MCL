using System.Text.Json;

namespace MCL.Core.MiniCommon;

public static class Json
{
    public static string Serialize<T>(T data, JsonSerializerOptions options = null)
    {
        return JsonSerializer.Serialize(data, options);
    }

    public static T Deserialize<T>(string json, JsonSerializerOptions options = null)
    {
        return JsonSerializer.Deserialize<T>(json, options);
    }

    public static void Save<T>(string filepath, T data)
    {
        string json = Serialize<T>(data);
        VFS.WriteFile(filepath, json);
    }

    public static void Save<T>(string filepath, T data, JsonSerializerOptions options = null)
    {
        if (!VFS.Exists(filepath))
            VFS.CreateDirectory(VFS.GetDirectoryName(filepath));

        VFS.WriteFile(filepath, Serialize(data, options));
    }

    public static T Load<T>(string filepath)
        where T : new()
    {
        if (!VFS.Exists(filepath))
        {
            Save(filepath, new T());
        }

        string json = VFS.ReadAllText(filepath);
        return Deserialize<T>(json);
    }

    public static T Load<T>(string filepath, JsonSerializerOptions options = null)
        where T : new()
    {
        if (!VFS.Exists(filepath))
        {
            Save(filepath, new T());
        }

        string json = VFS.ReadAllText(filepath);
        return Deserialize<T>(json, options);
    }
}
