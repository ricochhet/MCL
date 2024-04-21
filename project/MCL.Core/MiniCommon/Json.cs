using System.Text.Json;

namespace MCL.Core.MiniCommon;

public static class Json
{
    /// <summary>
    /// Serialize data of type T.
    /// </summary>
    public static string Serialize<T>(T data, JsonSerializerOptions options = null)
    {
        return JsonSerializer.Serialize(data, options);
    }

    /// <summary>
    /// Deserialize data of type T.
    /// </summary>
    public static T Deserialize<T>(string json, JsonSerializerOptions options = null)
    {
        return JsonSerializer.Deserialize<T>(json, options);
    }

    /// <summary>
    /// Serialize data of type T, and save to a file.
    /// </summary>

    public static void Save<T>(string filepath, T data)
    {
        string json = Serialize(data);
        VFS.WriteFile(filepath, json);
    }

    /// <summary>
    /// Serialize data of type T, and save to a file.
    /// </summary>
    public static void Save<T>(string filepath, T data, JsonSerializerOptions options)
    {
        if (!VFS.Exists(filepath))
            VFS.CreateDirectory(VFS.GetDirectoryName(filepath));

        VFS.WriteFile(filepath, Serialize(data, options));
    }

    /// <summary>
    /// Deserialize file text, and return as type T.
    /// </summary>
    public static T Load<T>(string filepath)
        where T : new()
    {
        if (!VFS.Exists(filepath))
            return default;

        string json = VFS.ReadAllText(filepath);
        try
        {
            return Deserialize<T>(json);
        }
        catch
        {
            return default;
        }
    }

    /// <summary>
    /// Deserialize file text, and return as type T.
    /// </summary>
    public static T Load<T>(string filepath, JsonSerializerOptions options)
        where T : new()
    {
        if (!VFS.Exists(filepath))
            return default;

        string json = VFS.ReadAllText(filepath);
        try
        {
            return Deserialize<T>(json, options);
        }
        catch
        {
            return default;
        }
    }
}
