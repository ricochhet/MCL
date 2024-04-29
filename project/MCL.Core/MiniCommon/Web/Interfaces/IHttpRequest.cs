using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MCL.Core.MiniCommon.Web.Interfaces;

public interface IHttpRequest
{
    /// <summary>
    /// Sends a GET async request to the specified URI.
    /// </summary>
    public static abstract Task<HttpResponseMessage?> GetAsync(string request);

    /// <summary>
    /// Sends a GET async request to the specified URI, and returns the response body as a byte array.
    /// </summary>
    public static abstract Task<byte[]?> GetByteArrayAsync(string request);

    /// <summary>
    /// Sends a GET async request to the specified URI, and returns the response body as a stream.
    /// </summary>
    public static abstract Task<Stream?> GetStreamAsync(string request);

    /// <summary>
    /// Sends a GET async request to the specified URI, and returns the response body as a deserialized object of type T.
    /// </summary>
    public static abstract Task<T?> GetObjectFromJsonAsync<T>(string request);

    /// <summary>
    /// Sends a GET async request to the specified URI, and saves the deserialized response of type T to a file.
    /// </summary>
    public static abstract Task<string?> GetJsonAsync<T>(string request, string filepath, Encoding encoding);

    /// <summary>
    /// Sends a GET async request to the specified URI, and saves the response to a file.
    /// </summary>
    public static abstract Task<string?> GetStringAsync(string request, string filepath, Encoding encoding);

    /// <summary>
    /// Sends a GET async request to the specified URI, and returns the response body as a string.
    /// </summary>
    public static abstract Task<string?> GetStringAsync(string request);

    /// <summary>
    /// Sends a GET async request to the specified URI, compares SHA256 hashes, and saves file if comparison is false.
    /// </summary>
    public static abstract Task<bool> DownloadSHA256(string request, string filepath, string hash);

    /// <summary>
    /// Sends a GET async request to the specified URI, compares SHA1 hashes, and saves file if comparison is false.
    /// </summary>
    public static abstract Task<bool> DownloadSHA1(string request, string filepath, string hash);

    /// <summary>
    /// Sends a GET async request to the specified URI, and saves the response as a filestream.
    /// </summary>
    public static abstract Task<bool> Download(string request, string filepath);
}
