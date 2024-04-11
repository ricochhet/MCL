using System;

namespace MCL.Core.Models.Web;

public class RequestData
{
    public string URL { get; set; }
    public string FilePath { get; set; }
    public int Size { get; set; }
    public string SHA1 { get; set; }
    public static event Action<RequestData> OnRequestDataAdded;

    public RequestData(string url, string filePath, int size, string sha1)
    {
        URL = url;
        FilePath = filePath;
        Size = size;
        SHA1 = sha1;
        OnRequestDataAdded?.Invoke(this);
    }
}
