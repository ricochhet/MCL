using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using MCL.Core.Helpers;
using MCL.Core.Logger;
using MCL.Core.Providers;

namespace MCL.Core.MiniCommon;

public static class Request
{
    public static async Task<string> DoRequest(string url, string fileName, Encoding enc)
    {
        try
        {
            using HttpClient client = new();
            string stringData = await client.GetStringAsync(new Uri(url));
            LogBase.Info($"Requesting data from URL:\n{url}");

            if (string.IsNullOrEmpty(stringData))
                return default;

            string existingSha1 = CryptographyHelper.Sha1(fileName);
            string downloadedSha1 = CryptographyHelper.Sha1(stringData, enc);
            if (FsProvider.Exists(fileName) && existingSha1 == downloadedSha1)
            {
                LogBase.Info($"File: {fileName} already exists.\n{existingSha1} == {downloadedSha1}");
                return stringData;
            }

            FsProvider.WriteFile(Path.GetDirectoryName(fileName), Path.GetFileName(fileName), stringData);
            return stringData;
        }
        catch (Exception ex)
        {
            LogBase.Error($"Request failed: {ex.Message}\nUrl: {url}");
            return default;
        }
    }

    public static async Task<T> DoRequest<T>(string url, string fileName, Encoding enc, JsonSerializerOptions options)
    {
        try
        {
            using HttpClient client = new();
            string jsonData = await client.GetStringAsync(new Uri(url));
            LogBase.Info($"Requesting data from URL:\n{url}");

            if (string.IsNullOrEmpty(jsonData))
                return default;

            string existingSha1 = CryptographyHelper.Sha1(fileName);
            string downloadedSha1 = CryptographyHelper.Sha1(jsonData, enc);
            if (FsProvider.Exists(fileName) && existingSha1 == downloadedSha1)
            {
                LogBase.Info($"File: {fileName} already exists.\n{existingSha1} == {downloadedSha1}");
                return JsonSerializer.Deserialize<T>(jsonData, options);
            }

            FsProvider.WriteFile(Path.GetDirectoryName(fileName), Path.GetFileName(fileName), jsonData);
            return JsonSerializer.Deserialize<T>(jsonData, options);
        }
        catch (Exception ex)
        {
            LogBase.Error($"Request failed: {ex.Message}\nUrl: {url}");
            return default;
        }
    }

    public static async Task<bool> Download(string downloadPath, string url, string sha1)
    {
        string existingSha1 = CryptographyHelper.Sha1(downloadPath);
        if (FsProvider.Exists(downloadPath) && existingSha1 == sha1)
        {
            LogBase.Info($"File: {downloadPath} already exists.\n{existingSha1} == {sha1}");
            return true;
        }
        else if (!await Download(url, downloadPath))
        {
            return false;
        }

        return true;
    }

    private static async Task<bool> Download(string url, string fileName)
    {
        try
        {
            using HttpClient httpClient = new();
            using HttpResponseMessage response = await httpClient.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                FsProvider.CreateDirectory(Path.GetDirectoryName(fileName));

                using Stream contentStream = await response.Content.ReadAsStreamAsync();
                using FileStream fileStream = new(fileName, FileMode.Create, FileAccess.Write, FileShare.None);
                LogBase.Info($"Downloading file:\nPath: {fileName}\nUrl: {url}");
                if (fileStream.Length == 0)
                    return false;
                await contentStream.CopyToAsync(fileStream);
                return true;
            }
            else
            {
                LogBase.Error($"Failed to download file.\nUrl: {url}\nStatus code: {response.StatusCode}");
                return false;
            }
        }
        catch (Exception ex)
        {
            LogBase.Error($"Failed to download file: {ex.Message}");
            return false;
        }
    }
}
